using Data.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Requests.Put
{
    public class CartItemUpdateModel
    {
        public ProductViewModel Product { get; set; } = null!;
        public int Quantity { get; set; }

    }
}
