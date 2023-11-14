using Beauty_Art.API.Constants;
using Beauty_Art.API.Controllers;
using Beauty_Art.API.Payload.Response;
using Beauty_Art.Payload.Request;
using Beauty_Art.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Beauty_Art.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletTypeController : BaseController<WalletTypeController>
    {
        private readonly IWalletTypeService _walletType;

        public WalletTypeController(IWalletTypeService service, ILogger<WalletTypeController> logger) : base(logger)
        {
            _walletType = service;
        }
        [HttpPost("create-wallet")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateWallet([FromBody]WalletTypeRequest req)
        {
            var wallet = await _walletType.CreateWallet(req);
            if (wallet == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.WalletType.CreateWalletFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(wallet);
        }
        [HttpDelete("delete-wallet/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteWallet(Guid id)
        {
            var result = await _walletType.DeleteWallet(id);
            if (!result)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.WalletType.DeleteWalletFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(result);
        }
    }
}
