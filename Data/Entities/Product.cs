using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Product
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int Price { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<FeverousItem> FeverousItems { get; set; } = new List<FeverousItem>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
