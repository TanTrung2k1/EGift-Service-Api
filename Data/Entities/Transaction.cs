namespace Data.Entities
{
    public partial class Transaction
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public int Amount { get; set; }

        public string TxnRef { get; set; } = null!;

        public virtual Order Order { get; set; } = null!;
    }
}
