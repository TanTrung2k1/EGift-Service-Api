namespace Data.Models.Requests.Post
{
    public class OrderCreateModel
    {
        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Receiver { get; set; } = null!;

        public string PaymentMethod { get; set; } = null!;
    }
}
