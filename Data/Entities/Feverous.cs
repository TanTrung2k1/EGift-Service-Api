using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Feverous
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<FeverousItem> FeverousItems { get; set; } = new List<FeverousItem>();
}
