using Beauty_Art.API.Constants;
using Beauty_Art.API.Controllers;
using Beauty_Art.API.Payload.Response;
using Beauty_Art.Domains.Models;
using Beauty_Art.Payload.Request;
using Beauty_Art.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Beauty_Art.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController<OrderController>
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService service, ILogger<OrderController> logger) : base(logger)
        {
            _orderService = service;
        }
        [HttpPost("payment")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        public async Task<IActionResult> Payment([FromBody]OrderRequest req)
        {
            var order = await _orderService.Payment(req);
            if (order == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Order.PaymentFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(order);
        }
    }
}
