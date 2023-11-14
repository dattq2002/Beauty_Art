using AutoMapper;
using Beauty_Art.API.Services;
using Beauty_Art.API.Utils;
using Beauty_Art.Domain.Paginate;
using Beauty_Art.Domains.Models;
using Beauty_Art.Payload.Request;
using Beauty_Art.Payload.Response;
using Beauty_Art.Repository.Interfaces;
using Beauty_Art.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Pos_System.API.Enums;

namespace Beauty_Art.Service.Implement
{
    public class AccountService : BaseService<AccountService>, IAccountService
    {
        public AccountService(IUnitOfWork<BEAUTIFUL_ARTSContext> unitOfWork, ILogger<AccountService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<LoginResponse>Login(LoginRequest req)
        {
            if(req.Username != "admin" && req.Password != "123456")
            req.Password = PasswordUtil.HashPassword(req.Password);
            Account account = await _unitOfWork.GetRepository<Account>()
                .SingleOrDefaultAsync(predicate: x => x.UserName == req.Username && x.Password == req.Password);
            if (account == null)
            {
                return new LoginResponse()
                {
                    Message = "Username or password is incorrect"
                };
            }
            Role roleAcc = await _unitOfWork.GetRepository<Role>().SingleOrDefaultAsync(predicate: x => x.Id.Equals(account.RoleId));
            RoleEnum role = EnumUtil.ParseEnum<RoleEnum>(roleAcc.Name);
            Tuple<string, Guid> guidClaim = null;
            LoginResponse loginResponse = null;
            switch (role)
            {
                case RoleEnum.Admin:
                    guidClaim = new Tuple<string, Guid>(Guid.NewGuid().ToString(), account.Id);
                    account.Role = roleAcc;
                    loginResponse = new LoginResponse()
                    {
                        Message = "Login successfully",
                        Account_Id = account.Id,
                        Username = account.UserName,
                        Role = role
                    };
                    break;
                case RoleEnum.Customer:
                    guidClaim = new Tuple<string, Guid>(Guid.NewGuid().ToString(), account.Id);
                    account.Role = roleAcc;
                    loginResponse = new LoginResponse()
                    {
                        Message = "Login successfully",
                        Account_Id = account.Id,
                        Username = account.UserName,
                        Role = role
                    };
                    break;
                case RoleEnum.Staff:
                    guidClaim = new Tuple<string, Guid>(Guid.NewGuid().ToString(), account.Id);
                    account.Role = roleAcc;
                    loginResponse = new LoginResponse()
                    {
                        Message = "Login successfully",
                        Account_Id = account.Id,
                        Username = account.UserName,
                        Role = role
                    };
                    break;
                default:
                    break;
            }
            return loginResponse;
        }

        public async Task<AccountRespone> Register(RegisterRequest req)
        {
            if(req.Password != req.Confirm_Password)
            {
                return null;
            }
            req.Password = PasswordUtil.HashPassword(req.Password);
            Account account = new Account()
            {
                Password = req.Password,
                UserName = req.Username,
                Id = Guid.NewGuid(),
                
            };
            account.InsDate = DateTime.Now;
            account.UpsDate = DateTime.Now;
            account.Deflag = false;
            Role role = await _unitOfWork.GetRepository<Role>().SingleOrDefaultAsync(predicate: x => x.Name == "User");
            account.RoleId = role.Id;
            await _unitOfWork.GetRepository<Account>().InsertAsync(account);
            await _unitOfWork.CommitAsync();
            //tạo mới user
            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                AccountId = account.Id,
                InsDate = DateTime.Now,
                UpsDate = DateTime.Now,
                Deflag = false,
                Email = req.Email,
                PhoneNumber = req.Phone,
                Gender = req.Gender == "Nam"? 0 : 1 
            };
            await _unitOfWork.GetRepository<User>().InsertAsync(newUser);
            await _unitOfWork.CommitAsync();
            await createWallet(newUser);
            return new AccountRespone()
            {
                Message = "Register successfully",
                username = account.UserName,
                Role = role.Name,
                Ins_Date = DateTime.Now,
                Upd_Date = DateTime.Now
            };
         }
        private async Task<UserWallet> createWallet(User req)
        {
            IPaginate<WalletType> paginate = await _unitOfWork.GetRepository<WalletType>()
                .GetPagingListAsync(predicate: x => (bool)!x.DeFlag, page: 1, size: 1);
            List<WalletType> walletTypes = paginate.Items.ToList();
            foreach(var wallet in walletTypes)
            {
                UserWallet userWallet = new UserWallet()
                {
                    Id = Guid.NewGuid(),
                    UserId = req.Id,
                    WalletTypeId = wallet.Id,
                    Balance = 0,
                    InsDate = DateTime.Now,
                    UpsDate = DateTime.Now,
                    DeFlag = false,
                    BalanceHistory = 0,
                    Name = wallet.Name,
                };
                await _unitOfWork.GetRepository<UserWallet>().InsertAsync(userWallet);
                await _unitOfWork.CommitAsync();
                return userWallet;
            }
            return null;
        }

        public async Task<User> GetUser(Guid Account_Id)
        {
            User user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: x => x.AccountId == Account_Id &&
            !x.Deflag);
            if(user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<IPaginate<Account>> GetListAccount(int page, int size)
        {
            IPaginate<Account> list = await _unitOfWork.GetRepository<Account>().
                GetPagingListAsync(predicate: x => !x.Deflag, page: page, size: size
                );
            return list;
        }
        public async Task<Account> UpdateAccount(AccountRequest req, Guid id)
        {
            Account account = await _unitOfWork.GetRepository<Account>()
                .SingleOrDefaultAsync(predicate: x => !x.Deflag && x.Id.Equals(id));
            if (account == null)
            {
                return null;
            }
            Role role = await _unitOfWork.GetRepository<Role>().SingleOrDefaultAsync(predicate: x => x.Name == req.Role.GetDescriptionFromEnum());
            account.RoleId = role.Id;
            account.UpsDate = DateTime.Now;
            _unitOfWork.GetRepository<Account>().UpdateAsync(account);
            await _unitOfWork.CommitAsync();
            return account;
        }
        public async Task<bool> DeleteAccount(Guid id)
        {
            Account account = await _unitOfWork.GetRepository<Account>().SingleOrDefaultAsync(
                               predicate: x => !x.Deflag && x.Id.Equals(id)
                                              );
            if (account == null)
            {
                return false;
            }
            account.Deflag = true;
            _unitOfWork.GetRepository<Account>().UpdateAsync(account);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
