﻿using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class ProductImage
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public string? Type { get; set; }

    public string? Url { get; set; }

    public virtual Product Product { get; set; } = null!;
}
