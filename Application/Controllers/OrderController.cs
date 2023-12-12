using Data.Models.Internal;
using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using StackExchange.Redis;

namespace Application.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var result = await _orderService.GetOrders();
                if (result is JsonResult jsonResult)
                {
                    return Ok(jsonResult.Value);
                }
                return BadRequest("Not found this order");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] Guid id)
        {
            try
            {
                var result = await _orderService.GetOrder(id);
                if (result is JsonResult jsonResult)
                {
                    return Ok(jsonResult.Value);
                }
                return BadRequest("Not found this order");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("current-customer")]
        public async Task<IActionResult> GetOrderOfCurrentCustomer()
        {
            try
            {
                var user = (AuthModel)HttpContext.Items["User"]!;
                if (user != null)
                {
                    var result = await _orderService.GetOrdersByCustomerId(user.Id);
                    if (result is JsonResult jsonResult)
                    {
                        return Ok(jsonResult.Value);
                    }
                    return NotFound("Not found");
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("customer/{id}")]
        public async Task<IActionResult> GetOrdersByCustomerId([FromRoute] Guid id)
        {
            try
            {
                var result = await _orderService.GetOrdersByCustomerId(id);
                if (result is JsonResult jsonResult)
                {
                    return Ok(jsonResult.Value);
                }
                return NotFound("Not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}/payment-url")]
        public async Task<IActionResult> GetPaymentUrlByOrderId([FromRoute] Guid id)
        {
            try
            {
                var remoteIp = HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();

                var paymentModel = await _orderService.GetOrderPaymentUrlByOrderId(id, remoteIp);

                return Ok(paymentModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Route("{cartId}")]
        public async Task<IActionResult> CreateOrder([FromRoute] Guid cartId, [FromBody] OrderCreateModel request)
        {
            try
            {
                var user = (AuthModel)HttpContext.Items["User"]!;
                if (user != null)
                {
                    var remoteIp = HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();

                    var orderCreateModelWithRemoteIp = new OrderCreateModelWithRemoteIp(remoteIp, request, user.Id);

                    var result = await _orderService.CreateOrder(cartId, orderCreateModelWithRemoteIp);

                    if (result is JsonResult jsonResult)
                    {
                        return StatusCode(StatusCodes.Status201Created, jsonResult.Value);
                    }
                    if (result is StatusCodeResult status)
                    {
                        if (status.StatusCode == StatusCodes.Status400BadRequest)
                        {
                            return BadRequest("Something wrong in Create order!!!");
                        }
                    }
                    return BadRequest("Something wrong!!!");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, UpdateOrderRequest request)
        {
            try
            {
                //redis distributed lock
                var redisConfiguration = ConfigurationOptions.Parse("localhost");
                var redis = ConnectionMultiplexer.Connect(redisConfiguration);
                if (redis.IsConnected)
                {
                    Console.WriteLine("Redis server connection successful!");
                }
                else
                {
                    Console.WriteLine("Redis server connection failed!");
                }
                var database = redis.GetDatabase();
                string lockKey = $"order_lock:{id}";

                //Set if Not Exists
                // Try to set the lock key with SETNX command (expiration of 10 seconds)
                bool isLockAcquired = database.StringSet(lockKey, id.ToString(), TimeSpan.FromSeconds(10), When.NotExists);
                if (!isLockAcquired)
                {
                    return StatusCode(StatusCodes.Status409Conflict, "Someone else is already update this item. Please try again later.");
                }
                try
                {
                    var result = await _orderService.UpdateOrder(id, request);
                    if (result is JsonResult jsonResult)
                    {
                        return StatusCode(StatusCodes.Status201Created, jsonResult.Value);
                    }
                    if (result is StatusCodeResult status)
                    {
                        if (status.StatusCode == StatusCodes.Status400BadRequest)
                        {
                            return BadRequest("Error when execute save to database");
                        }
                        if (status.StatusCode == 443)
                        {
                            return StatusCode(443, "Can't confirm because the order is in canceled status");
                        }
                        if (status.StatusCode == 444)
                        {
                            return StatusCode(444, "Can't cancel because the order is in confirmed status");
                        }
                        if (status.StatusCode == StatusCodes.Status409Conflict)
                        {
                            return StatusCode(StatusCodes.Status409Conflict, "Out of stock");
                        }
                    }
                    return BadRequest("Something wrong!!!");
                }
                finally
                {
                    database.KeyDelete(lockKey);
                }
                //var result = await _orderService.UpdateOrder(id, request);
                //if (result is JsonResult jsonResult)
                //{
                //    return StatusCode(StatusCodes.Status201Created, jsonResult.Value);
                //}
                //if (result is StatusCodeResult status)
                //{
                //    if (status.StatusCode == StatusCodes.Status400BadRequest)
                //    {
                //        return BadRequest("Error when execute save to database");
                //    }
                //    if (status.StatusCode == 443)
                //    {
                //        return StatusCode(443, "Can't confirm because the order is in canceled status");
                //    }
                //    if (status.StatusCode == 444)
                //    {
                //        return StatusCode(444, "Can't cancel because the order is in confirmed status");
                //    }
                //    if (status.StatusCode == StatusCodes.Status409Conflict)
                //    {
                //        return StatusCode(StatusCodes.Status409Conflict, "Out of stock");
                //    }
                //}
                //return BadRequest("Something wrong!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("cancel/{id}")]
        public async Task<IActionResult> CancelOrder(Guid id)
        {
            try
            {
                var user = (AuthModel)HttpContext.Items["User"]!;
                if (user != null)
                {
                    var rs = await _orderService.CancelOrder(new OrderCancelModel { CustomerId = user.Id, OrderId = id });
                    if (rs is JsonResult jsonResult)
                    {
                        return StatusCode(StatusCodes.Status200OK, jsonResult.Value);
                    }
                    if (rs is StatusCodeResult status)
                    {
                        switch (status.StatusCode)
                        {
                            case 400: return BadRequest(new { Message = "Order is canceled only when in Processing state." });
                            case 500: return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "InternalServerError" });
                        }
                    }
                    return NotFound(new { Message = "Not found order" });
                }
                return Unauthorized(new { Message = "Unauthorized" });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = e.Message });
            }
        }
    }
}
