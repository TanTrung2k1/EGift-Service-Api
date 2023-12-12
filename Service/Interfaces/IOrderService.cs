using Data.Models.Internal;
using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace Service.Interfaces
{
    public interface IOrderService
    {
        Task<IActionResult> GetOrder(Guid id);
        Task<IActionResult> GetOrders();
        Task<IActionResult> GetOrdersByCustomerId(Guid customerId);
        Task<IActionResult> CreateOrder(Guid cartId, OrderCreateModelWithRemoteIp request);
        Task<IActionResult> UpdateOrder(Guid id, UpdateOrderRequest request);
        Task<PaymentViewModel> GetOrderPaymentUrlByOrderId(Guid id, string remoteIpAddress);
        Task<IActionResult> CancelOrder(OrderCancelModel ocm);
    }
}
