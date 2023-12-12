using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Requests.Post
{
    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Address { get; set; }

    }
}
