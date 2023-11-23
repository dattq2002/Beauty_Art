using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Model;
using Services.Service.Interface;

namespace Project.SWP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _service;

        public WalletController(IWalletService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetWalletById([FromQuery] string id)
        {
            var result = await _service.GetWalletById(id);
            return Ok(result);
        }
        [HttpGet("GetWallets")]
        public async Task<IActionResult> GetWallets()
        {
            var result = await _service.GetWalletsAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateWallets([FromBody] WalletModel model)
        {
            var result = await _service.CreateWallets(model);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateWallets([FromBody] WalletModel model, [FromQuery] string id)
        {
            var result = await _service.UpdateWallets(model, id);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteWallets([FromQuery] string id)
        {
            var result = await _service.DeleteWallets(id);
            return Ok(result);
        }
    }
}
