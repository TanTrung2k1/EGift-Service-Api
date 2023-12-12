namespace Data.Models.View
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int Price { get; set; }

        public int Quantity { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public ICollection<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
        public ICollection<ProductImageViewModel> ProductImages { get; set; } = new List<ProductImageViewModel>();
    }
}
