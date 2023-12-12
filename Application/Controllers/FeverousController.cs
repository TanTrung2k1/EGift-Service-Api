using Data.Models.Filters;
using Data.Models.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Application.Controllers
{
    [Route("api/feverous")]
    [ApiController]
    public class FeverousController : ControllerBase
    {
        private IFeverousService _feverousService;

        public FeverousController(IFeverousService feverousService)
        {
            _feverousService = feverousService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFeverous()
        {
            try
            {
                var user = (AuthModel)HttpContext.Items["User"]!;
                if (user != null)
                {
                    var result = await _feverousService.GetFeverousByCustomerID(user.Id);
                    if (result is JsonResult jsonResult)
                    {
                        return Ok(jsonResult.Value);
                    }
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("{productId}")]
        public async Task<IActionResult> AddFeverous([FromRoute] Guid productId)
        {
            try
            {
                var user = (AuthModel)HttpContext.Items["User"]!;
                if(user != null)
                {
                    var resutl = await _feverousService.AddFeverous(user.Id, productId);
                    if(resutl is JsonResult jsonResult)
                    {
                        return StatusCode(StatusCodes.Status201Created, jsonResult.Value);
                    }
                    if(resutl is StatusCodeResult statusCodeResult)
                    {
                        var statusCode = statusCodeResult.StatusCode;
                        if (statusCode == StatusCodes.Status400BadRequest)
                        {
                            return BadRequest("Something wrong when save to database!!!");
                        }
                        if (statusCode == StatusCodes.Status400BadRequest)
                        {
                            return Conflict();
                        }
                    }
                }
                return Unauthorized();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{feverousItemId}")]
        public async Task<IActionResult> DeleteFeverousItem([FromRoute] Guid feverousItemId)
        {
            try
            {
                var user = (AuthModel)HttpContext.Items["User"]!;
                if(user != null)
                {
                    var result = await _feverousService.DeleteFeverousItem(user.Id, feverousItemId);
                    if(result is JsonResult jsonResult)
                    {
                        return StatusCode(StatusCodes.Status201Created, jsonResult.Value);
                    }
                    if (result is StatusCodeResult statusCodeResult)
                    {
                        if (statusCodeResult.StatusCode == StatusCodes.Status400BadRequest)
                        {
                            return BadRequest("Something wrong when save to database!!!");
                        }
                        if (statusCodeResult.StatusCode == StatusCodes.Status404NotFound)
                        {
                            return NotFound("Not found this product in feverous");
                        }
                    }
                    return BadRequest("Some thing wrong!!!");
                }
                return Unauthorized();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
