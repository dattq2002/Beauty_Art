using System.Security.Claims;
using AutoMapper;
using Beauty_Art.Domains.Models;
using Beauty_Art.Repository.Interfaces;

namespace Beauty_Art.API.Services
{
	public abstract class BaseService<T> where T : class
	{
		protected IUnitOfWork<BEAUTIFUL_ARTSContext> _unitOfWork;
		protected ILogger<T> _logger;
		protected IMapper _mapper;
		protected IHttpContextAccessor _httpContextAccessor;
		public BaseService(IUnitOfWork<BEAUTIFUL_ARTSContext> unitOfWork, ILogger<T> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
		}

		protected string GetUsernameFromJwt()
		{
			string username = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
			return username;
		}

		protected string GetRoleFromJwt()
		{
			string role = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
			return role;
		}
	}
}
