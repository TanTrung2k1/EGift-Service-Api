using Application.Configurations.Middleware;
using Data.Models.Internal;
using Data.Models.Requests.Post;
using Data.Models.View;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Application.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICustomerService _customerService;
        public AuthController(IAuthService authService, ICustomerService customerService)
        {
            _authService = authService;
            _customerService = customerService;
        }

        /// <summary>
        ///     Login to system
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(AuthViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AuthenticatedUser([FromBody][Required] AuthRequest auth)
        {
            var customer = await _authService.AuthenticatedUser(auth);
            if(customer != null)
            {
                return Ok(customer);
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet]
        [Authorize("Customer")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = (AuthModel)HttpContext.Items["User"]!;
            if(user != null)
            {
                return Ok(await _customerService.GetCustomer(user.Id));
            }
            return new StatusCodeResult(StatusCodes.Status401Unauthorized);
        }
    }
}
