using Data.Models.Filters;
using Data.Models.Request;
using Data.Models.Requests.Put;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Application.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService categoryService)
        {
            _service=categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory([FromQuery] CategoryFilterModel categoryFilter)
        {
            try
            {
                return await _service.GetCategory(categoryFilter);

            }catch (Exception ex)
            {
                return BadRequest("Can't get Categories, ERROR: "+ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateModel category)
        {
            try
            {
                var rs = await _service.CreateCategory(category);
                if(rs is JsonResult json)
                {
                    return Ok(json.Value);
                }
                return BadRequest("The Description and Name fields is required");
            }catch (Exception ex)
            {
                return BadRequest("Can't create category, ERROR: " + ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            try
            {
                var rs = await _service.GetCategoryById(id);
                if (rs is JsonResult json) { return Ok(json.Value); }
                return NotFound();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryUpdateModel categoryUpdateModel)
        {
            try
            {
                var rs =await _service.UpdateCategory(id, categoryUpdateModel);
                if (rs is JsonResult json) { 
                return StatusCode(StatusCodes.Status201Created, json.Value);
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
