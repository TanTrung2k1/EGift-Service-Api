using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Data.Models.Filters;
using Data.Models.Internal;
using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.View;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Utility.Enums;

namespace Service.Implementations
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IVoucherService _voucherService;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ICustomerRepository _customerRepository;

        private readonly ISendMailService _sendMailService;

        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IOrderDetailService orderDetailService, ISendMailService sendMailService, IPaymentService paymentService, IVoucherService voucherService) : base(unitOfWork, mapper)
        {
            _voucherService = voucherService;
            _voucherRepository = unitOfWork.Voucher;
            _orderRepository = unitOfWork.Order;
            _orderDetailService = orderDetailService;
            _productRepository = unitOfWork.Product;
            _cartRepository = unitOfWork.Cart;
            _cartItemRepository = unitOfWork.CartItem;
            _customerRepository = unitOfWork.Customer;
            _sendMailService = sendMailService;
            _paymentService = paymentService;
        }

        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderRepository.GetAll()
                        .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
                        .OrderByDescending(order => order.CreateAt)
                        .ToListAsync();
            if (orders != null)
            {
                return new JsonResult(orders);
            }
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        public async Task<IActionResult> GetOrder(Guid id)
        {
            var order = await _orderRepository.GetMany(order => order.Id.Equals(id))
                        .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
            if (order != null)
            {
                return new JsonResult(order);
            }
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        public async Task<IActionResult> GetOrderWithPaymentUrl(Guid id, string ipAddress)
        {
            var order = await _orderRepository.GetMany(order => order.Id.Equals(id))
                        .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();


            if (order != null)
            {

                var payment = new PaymentModel
                {
                    Amount = order.Amount,
                    IpAddress = ipAddress,
                    OrderId = order.Id
                };

                order.paymentUrl = _paymentService.GeneratePaymentUrl(payment);

                return new JsonResult(order);
            }
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        public async Task<IActionResult> CreateOrder(Guid cartId, OrderCreateModelWithRemoteIp request)
        {
            var cart = await _cartRepository.GetMany(cart => cart.Id.Equals(cartId))
                .Include(cart => cart.CartItems).ThenInclude(cartItem => cartItem.Product)
                .FirstOrDefaultAsync();


            //never write encapsulated code, much harder to read than this
            if (cart == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            //var voucher = await _voucherRepository.FirstOrDefaultAsync(x => x.Code.Equals(request.VoucherCode));
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = cart.CustomerId,
                Status = OrderStatus.Processing,
                Email = request.Email,
                Address = request.Address,
                Phone = request.Phone,
                Receiver = request.Receiver,
                //is paid ? this property use to identify using cash or VNpay right?
                //if it is right, it should not be named "is paid" maybe "PaymentMethod" is better
                IsPaid = false
            };

            _orderRepository.Add(order);

            foreach (var cartDetail in cart.CartItems)
            {
                //never encapsulated code AGAIN!
                if (cartDetail.Quantity == 0)
                {
                    continue;
                }

                var product = await _productRepository.GetMany(product => product.Id.Equals(cartDetail.ProductId)).FirstOrDefaultAsync();

                if (product == null)
                {
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                var orderDetail = new OrderDetailCreateModel
                {
                    ProductId = cartDetail.ProductId,
                    Quantity = cartDetail.Quantity,
                    Price = product.Price
                };
                var detail = await _orderDetailService.CreateOrderDetail(order.Id, orderDetail);
                if (detail == null)
                {
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                var amount = detail.Quantity * detail.Price;
                order.Amount += amount;
                //product.Quantity -= orderDetail.Quantity;
                //if (product.Quantity == 0)
                //{
                //product.Status = ProductStatus.RunOut;
                //}
                //_productRepository.Update(product);
            }
            //if (await _voucherService.CheckValidVoucher(request.VoucherCode, order.Amount) == null!)
            //{
            //    return new StatusCodeResult(StatusCodes.Status400BadRequest);
            //}

            //order.Amount = order.Amount - ((order.Amount * Convert.ToInt32(voucher.Discount)) / 100);
            //_orderRepository.Update(order);

            //voucher.Customers.Add(await _customerRepository.FirstOrDefaultAsync(x => x.Id.Equals(request.CustomerId)));
            //voucher.Quantity -= 1;

            foreach (var cartItem in cart.CartItems)
            {
                if (cartItem.Quantity != 0)
                {
                    _cartItemRepository.Remove(cartItem);
                }

            }

            var result = await _unitOfWork.SaveChanges();
            if (result <= 0)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }

            await SendMailConfirmOrder(order.Id);

            if (order.IsPaid)
            {
                return await GetOrderWithPaymentUrl(order.Id, request.RemoteIp);
            }
            else //we dont really need the "else" keyword, i know...
            {
                return await GetOrder(order.Id);
            }

        }

        private async Task SendMailConfirmOrder(Guid orderId)
        {
            var order = await _orderRepository.GetMany(order => order.Id.Equals(orderId))
                    .Include(order => order.Customer)
                    .Include(order => order.OrderDetails)
                        .ThenInclude(detail => detail.Product)
                        .ThenInclude(product => product.ProductImages)
                    .FirstOrDefaultAsync();

            if (order != null)
            {
                await _sendMailService.SendOrderConfirmationEmail(order);
            }
        }

        public async Task<IActionResult> GetOrdersByCustomerId(Guid customerId)
        {
            var orders = await _orderRepository.GetMany(order => order.CustomerId.Equals(customerId))
                .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider)
                .OrderByDescending(order => order.CreateAt)
                .ToListAsync();
            return new JsonResult(orders);
        }

        public async Task<IActionResult> UpdateOrder(Guid id, UpdateOrderRequest request)
        {
            var order = await _orderRepository.GetMany(order => order.Id.Equals(id)).Include(order => order.Customer)
                                                                                    .Include(order => order.OrderDetails)
                                                                                        .ThenInclude(detail => detail.Product)
                                                                                            .ThenInclude(product => product.ProductImages)
                                                                                    .FirstOrDefaultAsync();
            if (order != null)
            {
                if (request.Status.Contains(OrderStatus.Processing))
                {
                    order.Status = OrderStatus.Processing;
                }
                else if (request.Status.Contains(OrderStatus.Confirmed))
                {
                    if (order.Status.Contains(OrderStatus.Canceled))
                    {
                        return new StatusCodeResult(443);
                    }

                    order.Status = OrderStatus.Confirmed;
                    bool flag = await UpdateProductQuantity(order, OrderStatus.Confirmed);
                    if (!flag)
                    {
                        return new StatusCodeResult(StatusCodes.Status409Conflict);
                    }
                }
                else if (request.Status.Contains(OrderStatus.Transport))
                {
                    order.Status = OrderStatus.Transport;
                    await _sendMailService.SendMailNotifyForCustomerOrder(order);
                }
                else if (request.Status.Contains(OrderStatus.Finished))
                {
                    order.Status = OrderStatus.Finished;
                }
                else if (request.Status.Contains(OrderStatus.Canceled))
                {
                    if (order.Status.Contains(OrderStatus.Confirmed) || order.Status.Contains(OrderStatus.Transport))
                    {
                        return new StatusCodeResult(444);
                    }
                    order.Status = OrderStatus.Canceled;

                    //var voucherRollback = await _voucherService.RollBackVoucher(order.VoucherId, order.CustomerId);
                    //if (voucherRollback == null)
                    //{
                    //    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                    //}
                }
                _orderRepository.Update(order);
                var result = await _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    return await GetOrder(order.Id);
                }
            }
            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }

        private async Task<bool> UpdateProductQuantity(Order order, string status)
        {
            foreach (var detail in order.OrderDetails)
            {
                var product = await _productRepository.GetMany(product => product.Id.Equals(detail.ProductId)).FirstOrDefaultAsync();
                if (product != null)
                {
                    switch (status)
                    {
                        case OrderStatus.Confirmed:
                            if (product.Quantity < detail.Quantity)
                            {
                                return false;
                            }
                            product.Quantity -= detail.Quantity;
                            if (product.Quantity == 0)
                            {
                                product.Status = ProductStatus.RunOut;
                            }
                            break;
                        //case OrderStatus.Canceled:
                        //    product.Quantity += detail.Quantity;
                        //    if(product.Quantity >= 0)
                        //    {
                        //        product.Status = ProductStatus.Active;
                        //    }
                        //    break;
                        default:
                            break;
                    }
                    _productRepository.Update(product);
                }
            }
            return true;
        }

        public async Task<PaymentViewModel> GetOrderPaymentUrlByOrderId(Guid id, string remoteIpAddress)
        {
            var order = await _orderRepository.FirstOrDefaultAsync(order => order.Id == id);

            //fast fall pattern

            if (order == null)
            {
                throw new Exception(ResponseMessage.OrderNotFound);
            }

            if (!order.IsPaid)
            {
                throw new Exception(ResponseMessage.InvalidStateToGeneratePaymentUrl);
            }

            //========

            var paymentModel = new PaymentModel
            {
                Amount = order.Amount,
                IpAddress = remoteIpAddress,
                OrderId = order.Id
            };

            PaymentViewModel paymentViewModel = new PaymentViewModel
            {
                paymentUrl = _paymentService.GeneratePaymentUrl(paymentModel)
            };

            return paymentViewModel;
        }

        public async Task<IActionResult> CancelOrder(OrderCancelModel ocm)
        {
            var order = await _orderRepository.FirstOrDefaultAsync(x => x.Id.Equals(ocm.OrderId));
            if (order != null)
            {
                if (order.Status.Equals(OrderStatus.Processing))
                {
                    order.Status = OrderStatus.Canceled;
                    return await _unitOfWork.SaveChanges() > 0 ? await GetOrder(order.Id) : new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return null!;
        }
    }
}
