using Microsoft.AspNetCore.Mvc;
using Beauty_Art.API.Constants;

namespace Beauty_Art.API.Controllers
{
	[Route(ApiEndPointConstant.ApiEndpoint)]
	[ApiController]
	public class BaseController<T> : ControllerBase where T : BaseController<T>
	{
		protected ILogger<T> _logger;

		public BaseController(ILogger<T> logger)
		{
			_logger = logger;
		}
	}
}
