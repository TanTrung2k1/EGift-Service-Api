using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Requests.Post
{
    public class CartRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
