

namespace Data.Models.Internal
{
    public class PaymentModel
    {
        public Guid OrderId { get; set; }
        public int Amount { get; set; }
        public string IpAddress { get; set; } = null!;

    }
}
