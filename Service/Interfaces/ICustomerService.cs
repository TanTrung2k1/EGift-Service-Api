using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.View;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerViewModel> GetCustomer(Guid id);
        Task<IActionResult> RegisterCustomer(RegisterRequest request);
        Task<IActionResult> VerifyCustomer(string token);
        Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomerRequest request);
        Task<IActionResult> DeleteCustomer(Guid id);
        Task<IActionResult> UpdateCustomerPassword(Guid id, UpdatePasswordRequest request);
        Task<IActionResult> AddAvatar(Guid id, IFormFile file);
    }
}
