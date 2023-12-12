using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public string Status { get; set; } = null!;

    public int Amount { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public bool IsPaid { get; set; }

    public DateTime CreateAt { get; set; }

    public string Receiver { get; set; } = null!;

    public Guid? VoucherId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Voucher? Voucher { get; set; }
}
