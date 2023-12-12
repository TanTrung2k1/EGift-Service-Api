using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Data.Models.Requests.Post;
using Data.Models.View;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Service.Implementations
{
    public class OrderDetailService : BaseService, IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        public OrderDetailService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _orderDetailRepository = unitOfWork.OrderDetail;
        }

        public async Task<OrderDetailViewModel?> CreateOrderDetail(Guid orderId, OrderDetailCreateModel request)
        {
            var orderDetail = new OrderDetail
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Price = request.Price,
            };
            _orderDetailRepository.Add(orderDetail);
            var result = await _unitOfWork.SaveChanges();
            if(result > 0)
            {
                return await GetOrderDetail(orderDetail.Id);
            }
            return null;
        }

        public async Task<OrderDetailViewModel?> GetOrderDetail(Guid id)
        {
            return await _orderDetailRepository.GetMany(orderDetail => orderDetail.Id.Equals(id))
                .ProjectTo<OrderDetailViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }
    }
}
