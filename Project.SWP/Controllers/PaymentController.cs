using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Entity;
using Services.Service.Interface;

namespace Project.SWP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }
        [HttpGet("GetAllPayments")]
        public async Task<IActionResult> GetAllPayment()
        {
            List<Payment> paymentList = await _service.GetAllPayment();
            return Ok(paymentList);
        }
        [HttpGet]
        public async Task<IActionResult> GetPaymentById([FromQuery]string id)
        {
            Payment payment = await _service.GetPaymentById(id);
            return Ok(payment);
        }
        [HttpGet("TotalSale")]
        public async Task<IActionResult> TotalSale()
        {
            var total = await _service.TotalSale();
            return Ok(total);
        }
    }
}
