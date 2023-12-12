using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Customer
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? AvatarUrl { get; set; }

    public bool? IsActive { get; set; }

    public string VerifyToken { get; set; } = null!;

    public DateTime? VerifyTime { get; set; }

    public DateTime CreateAt { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual Feverous? Feverous { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
}
