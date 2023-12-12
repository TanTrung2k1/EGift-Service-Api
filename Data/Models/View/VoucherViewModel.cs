namespace Data.Models.View
{
    public class VoucherViewModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = null!;

        public double Discount { get; set; }

        public string Description { get; set; } = null!;

        public int Quantity { get; set; }

        public int FromPrice { get; set; }

        public int ToPrice { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
