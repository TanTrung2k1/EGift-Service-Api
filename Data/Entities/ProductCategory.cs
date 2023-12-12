using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class ProductCategory
{
    public Guid ProductId { get; set; }

    public Guid CategoryId { get; set; }

    public string? Description { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
