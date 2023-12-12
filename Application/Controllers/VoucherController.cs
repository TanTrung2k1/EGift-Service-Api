using Data.Models.Filters;
using Data.Models.Internal;
using Data.Models.Requests.Post;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Application.Controllers
{
    [Route("api/vouchers")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVoucher([FromQuery]VoucherCreateModel vcm)
        {
            try
            {
                var rs = await _voucherService.CreateVoucher(vcm);
                if (rs is JsonResult json)
                {
                    return StatusCode(StatusCodes.Status201Created, json.Value);
                }
                return BadRequest(new { Message = "Product or Category are required" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVoucher([FromRoute] Guid id)
        {
            try
            {
                var rs = await _voucherService.GetVoucher(id);
                if (rs is JsonResult json)
                {
                    return Ok(json.Value);
                }
                return NotFound(new { Message = "NotFound" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = e.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetVouchers([FromQuery] VoucherFilterModel vfm)
        {
            try
            {
                var user = (AuthModel)HttpContext.Items["User"]!;
                string cusId = null!;
                if (user != null)
                {
                    cusId = user.Id.ToString();
                }
                var rs = await _voucherService.GetVouchers(vfm, cusId);
                if (rs is JsonResult json)
                {
                    return Ok(json.Value);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = e.Message });
            }
        }
    }
}
