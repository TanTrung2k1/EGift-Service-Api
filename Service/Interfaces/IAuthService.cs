using Data.Models.Internal;
using Data.Models.Requests.Post;
using Data.Models.View;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        Task<AuthViewModel> AuthenticatedUser(AuthRequest auth);
        Task<AuthModel?> GetCustomerById(Guid id);
    }
}
