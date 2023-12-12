namespace Data.Models.Requests.Put
{
    public class OrderCancelModel
    {
        public Guid CustomerId { get; set; }

        public Guid OrderId { get; set; }
    }
}
