using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Voucher
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public double Discount { get; set; }

    public string Description { get; set; } = null!;

    public int Quantity { get; set; }

    public int FromPrice { get; set; }

    public int ToPrice { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime CreateAt { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
