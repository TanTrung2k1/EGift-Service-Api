using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Filters
{
    public class CartFilterModel
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; } 
        public int Quantity { get; set; }
    }
}
