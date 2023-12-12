namespace Data.Models.Requests.Post
{
    public class OrderDetailCreateModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
