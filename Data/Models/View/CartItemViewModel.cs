
namespace Data.Models.View
{
    public class CartItemViewModel
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public CartProductViewModel Product { get; set; } = null!;

    }
}
