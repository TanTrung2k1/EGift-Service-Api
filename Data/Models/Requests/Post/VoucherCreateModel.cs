namespace Data.Models.Requests.Post
{
    public class VoucherCreateModel
    {
        public double Discount { get; set; }

        public string Description { get; set; } = null!;

        public int Quantity { get; set; }

        public int FromPrice { get; set; }

        public int ToPrice { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
