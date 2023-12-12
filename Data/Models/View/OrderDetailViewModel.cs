namespace Data.Models.View
{
    public class OrderDetailViewModel
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public DateTime CreateAt { get; set; }

        public FeverousProductViewModel Product { get; set; } = null!;
    }
}
