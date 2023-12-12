using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Requests.Put
{
    public class UpdateCustomerRequest
    {
        public string? Name { get; set; }
        [Phone]
        public string? Phone { get; set; }
        public string? Address { get; set; }

        
    }
}
