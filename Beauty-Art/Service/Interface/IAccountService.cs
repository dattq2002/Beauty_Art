using Beauty_Art.Domain.Paginate;
using Beauty_Art.Domains.Models;
using Beauty_Art.Payload.Request;
using Beauty_Art.Payload.Response;

namespace Beauty_Art.Service.Interface
{
    public interface IAccountService
    {
        Task<AccountRespone> Register(RegisterRequest req);
        Task<LoginResponse> Login(LoginRequest req);
        Task<User> GetUser(Guid Account_Id);
        Task<IPaginate<Account>> GetListAccount(int page, int size);
        Task<Account> UpdateAccount(AccountRequest req, Guid id);
        Task<bool> DeleteAccount(Guid id);
    }
}
