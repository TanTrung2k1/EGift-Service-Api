
using Data.Models.Requests.Post;

namespace Data.Models.Internal
{
    public class OrderCreateModelWithRemoteIp : OrderCreateModel
    {
        public string RemoteIp { get; set; } = null!;
        public Guid CustomerId { get; set; }


        public OrderCreateModelWithRemoteIp(string remoteIp, OrderCreateModel orderCreateModel, Guid customerId)
        {
            RemoteIp = remoteIp;
            Email = orderCreateModel.Email;
            Address = orderCreateModel.Address;
            Phone = orderCreateModel.Phone;
            Receiver = orderCreateModel.Receiver;
            PaymentMethod = orderCreateModel.PaymentMethod;
            CustomerId = customerId;
        }
    }
}
