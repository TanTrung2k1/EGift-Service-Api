using Application.Configurations.Middleware;
using Data.Models.Filters;
using Data.Models.Internal;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Data.Models.Requests.Post;
using Data.Models.Requests.Put;

namespace Application.Controllers
{
    [Route("api/carts")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartService _service;

        public CartController(ICartService cartService)
        {
            _service = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(CartRequest cartRequest)
        {
            try
            {
                var user = (AuthModel)HttpContext.Items["User"]!;
                if (user != null)
                {
                    var rs = await _service.AddToCart(new CartFilterModel { CustomerId = user.Id, ProductId = cartRequest.ProductId, Quantity = cartRequest.Quantity });
                    if (rs is StatusCodeResult status)
                    {
                        if (status.StatusCode == 500) { return StatusCode(StatusCodes.Status500InternalServerError, new JsonResult(new { Message = "Can't add product to cart" })); }
                        if (status.StatusCode == 400) { return StatusCode(StatusCodes.Status400BadRequest, new ObjectResult(new { Message = "Product not exist or quantity < 1" }).Value); }
                    }
                    if (rs is JsonResult json)
                    {
                        if (json.Value is null) { return BadRequest("The product is out of stock"); }
                        if (json.Value != null) { return StatusCode(StatusCodes.Status201Created, json.Value); }
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest("Can't add to cart, ERROR: " + ex.Message);
            }
        }


        [HttpGet]
        [Authorize("Customer")]
        public async Task<IActionResult> GetItemsCart()
        {
            try
            {
                var user = (AuthModel)HttpContext.Items["User"]!;
                if (user != null)
                {
                    var rs = await _service.ListItemCart(user.Id);
                    if (rs is JsonResult jsonResult)
                    {
                        return jsonResult.Value == null ? NotFound(new { Message = "Cart not create yet" }) : Ok(jsonResult.Value);
                    }
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize("Customer")]
        public async Task<IActionResult> UpdateCart(CartUpdateModel cartUpdate)
        {
            try
            {
                var user = (AuthModel)HttpContext.Items["User"]!;
                if (user != null)
                {
                    var rs = await _service.UpdateCart(user.Id, cartUpdate);
                    if (rs is JsonResult jsonResult)
                    {
                        return StatusCode(StatusCodes.Status201Created, jsonResult.Value);
                    }
                    if (rs is StatusCodeResult status)
                    {
                        if (status.StatusCode == 400) { return BadRequest(new { Message = "Out of stock" }); }
                        if (status.StatusCode == 500) { return BadRequest(new { Message = "Can't update" }); }
                    }
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
