using System;
using System.Collections.Generic;

namespace Data.Entities;

public partial class Admin
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public DateTime CreateAt { get; set; }
}
