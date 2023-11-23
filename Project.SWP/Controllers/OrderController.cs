using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Model;
using Services.Service.Interface;

namespace Project.SWP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderModel req)
        {
            var result = await _service.CreateOrder(req);
            return Ok(result);
        }
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrder()
        {
            var result = await _service.GetAllOrder();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetOrderById([FromQuery]string id)
        {
            var result = await _service.GetOrderById(id);
            return Ok(result);
        }
    }
}
