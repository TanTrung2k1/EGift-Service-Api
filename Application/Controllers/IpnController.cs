using Data.Models.Requests.Get;
using Microsoft.AspNetCore.Mvc;

using Service.Interfaces;

namespace Application.Controllers
{
    [Route("api/ipn")]
    [ApiController]
    public class IpnController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public IpnController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpGet]
        public IActionResult IpnCallback([FromQuery] IpnModel ipnModel) {

            var ipnResponseViewModel = _paymentService.ProcessIpnCallback(ipnModel, HttpContext.Request.Query);

            return Ok(ipnResponseViewModel);
        }

    }
}
