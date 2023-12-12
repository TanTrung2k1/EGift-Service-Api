using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enums;

namespace Data.Models.View
{
    public class CustomerViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public CartViewModel Cart { get; set; } = null!;

        public FeverousViewModel Feverous { get; set; } = null!;

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? AvatarUrl { get; set; }

        public DateTime CreateAt { get; set; }

    }
}
