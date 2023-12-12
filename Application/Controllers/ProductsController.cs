using Data.Models.Filters;
using Data.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Application.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public Task<IActionResult> GetProducts([FromQuery] ProductFilterModel filter) {
            return _productService.GetProducts(filter);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<IActionResult> GetProduct([FromRoute] Guid id)
        {
            return _productService.GetProduct(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateModel product)
        {
            try
            {
                var  rs= await _productService.CreateProduct(product);
                if (rs is JsonResult jsonResult)
                {
                    if (jsonResult.Value is null) return BadRequest("Message: Quantity>0, Price>0");
                    return StatusCode(StatusCodes.Status201Created, jsonResult.Value);
                }
                return StatusCode(StatusCodes.Status400BadRequest, "Please input four image");
            }
            catch(Exception ex) { }
            {
                return BadRequest("Can't create product");
            }
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromForm]ProductUpdateModel productUpdateModel)
        {
            try
            {
                var rs = await _productService.UpdateProduct(id, productUpdateModel);
                if (rs is JsonResult jsonResult)
                {  
                    return StatusCode(StatusCodes.Status201Created, jsonResult.Value);
                }else if (rs is StatusCodeResult status)
                {
                    if (status.StatusCode == 500) return StatusCode(StatusCodes.Status500InternalServerError);
                    if (status.StatusCode == 400) return StatusCode(StatusCodes.Status400BadRequest, "Please input four image");

                }
                return BadRequest("Can't update product");

            }catch(Exception ex)
            {
                return BadRequest("Can't update product, ERROR: " + ex.Message);
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveProduct([FromRoute] Guid id)
        {
            try
            {
                var rs =await _productService.RemoveProduct(id);
                if (rs is JsonResult jsonResult)
                {
                    if (jsonResult.StatusCode == 500) return BadRequest("Can't remove product");
                    if (jsonResult.Value is null) return BadRequest("Product Id not exist");
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            catch (Exception ex)
            {
                return BadRequest("Can't remove product, ERROR: " + ex.Message);
            }
        }
    }
}
