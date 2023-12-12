using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class FeverousItem
{
    public Guid Id { get; set; }

    public Guid FeverousId { get; set; }

    public Guid ProductId { get; set; }

    public DateTime CreateAt { get; set; }

    public virtual Feverous Feverous { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
