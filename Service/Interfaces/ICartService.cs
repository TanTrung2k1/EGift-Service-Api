using Data.Models.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models.View;
using Data.Models.Requests.Put;

namespace Service.Interfaces
{
    public interface ICartService
    {
        public Task<IActionResult> AddToCart(CartFilterModel cartFilter);
        public Task<IActionResult> ListItemCart(Guid customerId);
        public Task<IActionResult> UpdateCart(Guid customerId, CartUpdateModel cum);
    }
}
