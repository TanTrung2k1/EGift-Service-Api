using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Requests.Post
{
    public class AuthRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
