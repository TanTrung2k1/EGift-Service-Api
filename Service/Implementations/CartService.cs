using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Data.Models.Filters;
using Data.Models.Requests.Put;
using Data.Models.View;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Service.Implementations
{
    public class CartService : BaseService, ICartService
    {
        private IMapper _mapper;
        private IUnitOfWork _context;

        private ICartRepository _cartRepository;
        private ICartItemRepository _cartItemRepository;
        private IProductRepository _productRepository;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _mapper = mapper;
            _context = unitOfWork;
            _cartRepository = unitOfWork.Cart;
            _cartItemRepository = unitOfWork.CartItem;
            _productRepository = unitOfWork.Product;
        }
        public async Task<IActionResult> AddToCart(CartFilterModel cartFilter)
        {
            Product product = await _productRepository.FirstOrDefaultAsync(x => x.Id.Equals(cartFilter.ProductId));
            if (cartFilter.Quantity > product.Quantity)
            {
                return new JsonResult(null);
            }
            if (product == null || cartFilter.Quantity < 1)
            {
                return new StatusCodeResult(400);
            }
            if (product.Quantity > 0) // Check valid Product
            {
                if (!_cartRepository.Contains(x => x.CustomerId.Equals(cartFilter.CustomerId))) // Check exist Cart
                {
                    Cart cart = new Cart
                    {
                        Id = Guid.NewGuid(),
                        CustomerId = cartFilter.CustomerId
                    };
                    CartItem cartItem = new CartItem
                    {
                        Id = Guid.NewGuid(),
                        CartId = cart.Id,
                        ProductId = cartFilter.ProductId,
                        Quantity = cartFilter.Quantity,
                        CreateAt = DateTime.Now
                    };
                    _cartRepository.Add(cart);
                    _cartItemRepository.Add(cartItem);
                    return await _context.SaveChanges() > 0 ? new JsonResult((await ListItemCart(cartFilter.CustomerId) as JsonResult ?? null!).Value) : new StatusCodeResult(500);
                }
                else
                {
                    var cart = await _cartRepository.FirstOrDefaultAsync(x => x.CustomerId.Equals(cartFilter.CustomerId), "CartItems");
                    CartItem cartItem = new CartItem
                    {
                        Id = Guid.NewGuid(),
                        CartId = cart.Id,
                        ProductId = cartFilter.ProductId,
                        Quantity = cartFilter.Quantity,
                        CreateAt = DateTime.Now
                    };
                    if (!_cartItemRepository.Contains(x => x.CartId.Equals(cart.Id))) // Check exist CartItem by IdCart
                    {
                        _cartItemRepository.Add(cartItem);
                        return await _context.SaveChanges() > 0 ? new JsonResult((await ListItemCart(cartFilter.CustomerId) as JsonResult ?? null!).Value) : new StatusCodeResult(500);
                    }
                    else
                    {
                        CartItem itemCart = await _cartItemRepository.FirstOrDefaultAsync(x => x.ProductId.Equals(cartFilter.ProductId) && x.CartId.Equals(cart.Id));
                        var productCheck = await _productRepository.FirstOrDefaultAsync(x => x.Id.Equals(cartItem.ProductId));
                        if (itemCart != null)
                        {
                            if (itemCart.Quantity >= productCheck.Quantity)
                            {
                                return new JsonResult(null);
                            }
                            foreach (var item in cart.CartItems)
                            {
                                if (item.ProductId.Equals(cartItem.ProductId))
                                {
                                    cartItem.Quantity += item.Quantity;
                                    if (cartItem.Quantity > (productCheck.Quantity))
                                    {
                                        cartItem.Quantity = productCheck.Quantity;
                                    }
                                }
                            }
                            if (cartItem.Quantity > 0)
                            {
                                itemCart.Quantity = cartItem.Quantity;
                                await _context.SaveChanges();
                                return await ListItemCart(cartFilter.CustomerId);
                            }
                            else
                            {
                                _cartItemRepository.Remove(itemCart);
                                await _context.SaveChanges();
                                return new StatusCodeResult(204);
                            }
                        }
                        else // Add new cartItem
                        {
                            if (cartItem.Quantity > 0)
                            {
                                _cartItemRepository.Add(cartItem);
                                await _context.SaveChanges();
                                return await ListItemCart(cartFilter.CustomerId);
                            }
                            return new JsonResult(null);
                        }
                    }
                }
            }
            else
            {
                return new JsonResult(null);
            }
        }

        public async Task<IActionResult> ListItemCart(Guid customerId)
        {
            var cart = await _cartRepository.FirstOrDefaultAsync(cart => cart.CustomerId.Equals(customerId));
            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customerId,
                };
                _cartRepository.Add(cart);
                await _context.SaveChanges();
            }
            CartViewModel cartViewModels = new CartViewModel();
            cartViewModels.Id = cart.Id;
            var cartItem = _cartItemRepository.GetMany(x => x.CartId.Equals(cart.Id)).ProjectTo<CartItemViewModel>(_mapper.ConfigurationProvider).ToList();
            cartViewModels.CartItems = cartItem!;
            return new JsonResult(cartViewModels);
        }

        //public async Task<IActionResult> UpdateCartItem(Guid customerId, CartUpdateViewModel cum)
        //{
        //    var cart = await _cartRepository.FirstOrDefaultAsync(q => q.Id == cum.CartId, "CartItems");
        //    if (cart != null)
        //    {
        //        _context.CartItem.RemoveRange(cart.CartItems);
        //        foreach (var item in cum.CartItems)
        //        {
        //            CartItem cartItem = new CartItem
        //            {
        //                Id = Guid.NewGuid(),
        //                CartId = cum.CartId,
        //                ProductId = item.ProductId,
        //                Quantity = item.Quantity,
        //                CreateAt = DateTime.Now
        //            };
        //            _context.CartItem.Add(cartItem);
        //        }
        //        return await _context.SaveChanges() > 0 ? new JsonResult((await ListItemCart(customerId) as JsonResult ?? null!).Value) : new StatusCodeResult(500);
        //    }
        //    return new StatusCodeResult(500);
        //}

        public async Task<IActionResult> UpdateCart(Guid customerId, CartUpdateModel model)
        {
            var cart = await _cartRepository.GetMany(cart => cart.CustomerId.Equals(customerId))
                .Include(cart => cart.CartItems)
                .FirstOrDefaultAsync();
            if (cart != null)
            {
                foreach (var oldItem in cart.CartItems.ToList())
                {
                    bool itemExistsInModel = false;

                    foreach (var newItem in model.CartItems)
                    {
                        if (oldItem.ProductId.Equals(newItem.Product.Id))
                        {
                            if ((await _productRepository.FirstOrDefaultAsync(x => x.Id.Equals(newItem.Product.Id)))!.Quantity < newItem.Quantity)
                            {
                                return new StatusCodeResult(400);
                            }
                            oldItem.Quantity = newItem.Quantity;
                            itemExistsInModel = true;
                            break;
                        }
                    }

                    if (!itemExistsInModel)
                    {
                        cart.CartItems.Remove(oldItem);
                    }
                }
                _cartRepository.Update(cart);
                var result = await _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    return await ListItemCart(customerId);
                }
            }
            return new StatusCodeResult(500);
        }
    }
}
