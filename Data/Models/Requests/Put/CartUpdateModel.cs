using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Requests.Put
{
    public class CartUpdateModel
    {
        public Guid CartId { get; set; }
        public List<CartItemUpdateModel> CartItems { get; set; } = null!;
    }
}
