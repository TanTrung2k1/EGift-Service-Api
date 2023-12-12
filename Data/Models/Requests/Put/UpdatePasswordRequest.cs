using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Requests.Put
{
    public class UpdatePasswordRequest
    {
       

        [Required]
        public string OldPassword { get; set; } = null!;

        public string NewPassword { get; set; } = null!;

    }
}
