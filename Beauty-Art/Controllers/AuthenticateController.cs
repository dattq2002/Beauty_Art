using Beauty_Art.API.Constants;
using Beauty_Art.API.Controllers;
using Beauty_Art.API.Payload.Response;
using Beauty_Art.Domain.Paginate;
using Beauty_Art.Domains.Models;
using Beauty_Art.Payload.Request;
using Beauty_Art.Payload.Response;
using Beauty_Art.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Beauty_Art.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : BaseController<AuthenticateController>
    {
        private IAccountService _accountService;

        public AuthenticateController( IAccountService accountService, ILogger<AuthenticateController> logger) : base(logger)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var loginResponse = await _accountService.Login(loginRequest);
            if (loginResponse == null)
            {
                return Unauthorized(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Error = MessageConstant.LoginMessage.InvalidUsernameOrPassword,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(loginResponse);
        }
        [HttpPost("register")]
        [ProducesResponseType(typeof(AccountRespone), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var accountResponse = await _accountService.Register(registerRequest);
            if (accountResponse == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Account.CreateAccountFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(accountResponse);
        }
        [HttpGet("get-user")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser(Guid Account_Id)
        {
            var user = await _accountService.GetUser(Account_Id);
            if (user == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Account.GetUserFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(user);
        }
        [HttpGet("get-list-account")]
        [ProducesResponseType(typeof(IPaginate<Account>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListAccount(int page, int size)
        {
            var listAccount = await _accountService.GetListAccount(page, size);
            if (listAccount == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Account.GetListAccountFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(listAccount);
        }
        [HttpPut("update-account")]
        [ProducesResponseType(typeof(Account), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAccount(AccountRequest req, Guid id)
        {
            var account = await _accountService.UpdateAccount(req, id);
            if (account == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Account.UpdateAccountFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(account);
        }
        [HttpDelete("delete-account/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var result = await _accountService.DeleteAccount(id);
            if (!result)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Account.DeleteAccountFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(result);
        }
    }
}
