using Data.Models.Internal;
using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.View;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Utility.Enums;

namespace Application.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPut]
        [Route("change-avatar")]
        [ProducesResponseType(typeof(CustomerViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeAvatar(IFormFile avatar)
        {
            if (avatar == null || avatar.Length <= 0)
            {
                return BadRequest(ResponseMessage.NoFile);
            }

            var user = (AuthModel)HttpContext.Items["User"]!;
            if (user != null)
            {
                try
                {
                    var result = await _customerService.AddAvatar(user.Id, avatar);
                    if (result is StatusCodeResult statusCodeResult)
                    {
                        var statusCode = statusCodeResult.StatusCode;
                        if (statusCode == StatusCodes.Status400BadRequest)
                        {
                            return StatusCode(StatusCodes.Status400BadRequest, ResponseMessage.Error);
                        }
                    }
                        return StatusCode(StatusCodes.Status201Created, await _customerService.GetCustomer(user.Id));
                }
                catch(Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
                }
            }
            return Unauthorized();
        }


        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterRequest), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[SwaggerOperation]
        public async Task<IActionResult> RegisterCustomer(RegisterRequest request)
        {
            var result = await _customerService.RegisterCustomer(request);
            

            //if (result is JsonResult json)
            //{
                //return StatusCode(StatusCodes.Status200OK, result);                          
            //}

            if (result is StatusCodeResult statusCodeResult)
            {
                var statusCode = statusCodeResult.StatusCode;
                if(statusCode == StatusCodes.Status409Conflict) 
                {
                    return StatusCode(StatusCodes.Status409Conflict);
                }
                if(statusCode == StatusCodes.Status201Created)
                {
                    return StatusCode(StatusCodes.Status201Created, new { Message = "success" });
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpGet("verify/{token}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> VerifyCustomer([FromRoute] string token)
        {
            var result = await _customerService.VerifyCustomer(token);
            if(result is StatusCodeResult statusCodeResult)
            {
                var statusCode = statusCodeResult.StatusCode;
                if(statusCode == StatusCodes.Status400BadRequest)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ResponseMessage.Error);
                }
                if(statusCode == StatusCodes.Status200OK)
                {
                    return StatusCode(StatusCodes.Status200OK, new { Message = "success" });
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, ResponseMessage.Error);
        }


        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(CustomerViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid id,
                                                        [FromBody] UpdateCustomerRequest request)
        {
            var result =  await _customerService.UpdateCustomer(id, request);
            
            
            if (result is StatusCodeResult statusCodeResult)
            {
                var statusCode = statusCodeResult.StatusCode;
                if(statusCode == StatusCodes.Status404NotFound)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ResponseMessage.CustomerNotFound);
                }
                
                if (statusCode == StatusCodes.Status201Created)
                {
                    return StatusCode(StatusCodes.Status201Created, await _customerService.GetCustomer(id));
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, ResponseMessage.Error);

        }

        [HttpPut]
        [Route("change-password/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCustomerPassword([FromRoute] Guid id,
                                                        [FromBody] UpdatePasswordRequest request)
        {
            var result = await _customerService.UpdateCustomerPassword(id, request);
            if(result is StatusCodeResult statusCodeResult)
            {
                var statusCode = statusCodeResult.StatusCode;
                if(statusCode == StatusCodes.Status404NotFound)
                {
                    return StatusCode(StatusCodes.Status404NotFound, ResponseMessage.CustomerNotFound);
                }
                if(statusCode == StatusCodes.Status409Conflict)
                {
                    return StatusCode(StatusCodes.Status409Conflict, "Old password not correct.");
                }if(statusCode == StatusCodes.Status201Created)
                {
                    return StatusCode(StatusCodes.Status201Created, ResponseMessage.Success);
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, ResponseMessage.Error);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer([FromRoute] Guid id)
        {
            var result = await _customerService.GetCustomer(id);
            if(result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomer([FromRoute] Guid id)
        {
            var result = await _customerService.DeleteCustomer(id);
            if(result is StatusCodeResult statusCodeResult)
            {
                var statusCode = statusCodeResult.StatusCode;
                if(statusCode == StatusCodes.Status404NotFound) 
                {
                    return StatusCode(StatusCodes.Status404NotFound, ResponseMessage.CustomerNotFound);
                }
                if(statusCode == StatusCodes.Status204NoContent) 
                {
                    return StatusCode(StatusCodes.Status204NoContent, ResponseMessage.Success);
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, ResponseMessage.Error);
        }

    }
}
