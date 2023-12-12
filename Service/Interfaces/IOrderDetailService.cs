using Data.Models.Requests.Post;
using Data.Models.View;

namespace Service.Interfaces
{
    public interface IOrderDetailService
    {
        Task<OrderDetailViewModel?> CreateOrderDetail(Guid orderId, OrderDetailCreateModel request);
        Task<OrderDetailViewModel?> GetOrderDetail(Guid id);
    }
}
