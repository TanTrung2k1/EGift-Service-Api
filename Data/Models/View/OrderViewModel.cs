using Data.Entities;

namespace Data.Models.View
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public virtual CustomerOrderViewModel Customer { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Address { get; set; } = null!;

        public bool IsPaid { get; set; }

        public string Receiver { get; set; } = null!;

        public string Status { get; set; } = null!;

        public int Amount { get; set; }

        public double Discount { get; set; }

        public DateTime CreateAt { get; set; }

        public string? paymentUrl { get; set; }

        public virtual ICollection<OrderDetailViewModel> OrderDetails { get; set; } = new List<OrderDetailViewModel>();
    }
}
