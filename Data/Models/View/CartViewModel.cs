using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.View
{
    public class CartViewModel
    {
        public Guid Id;
        public ICollection<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();

    }
}
