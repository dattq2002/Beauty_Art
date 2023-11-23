using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Services.Commons;
using Services.Entity;
using Services.Enum;
using Services.Model;
using Services.Service.Interface;

namespace Services.Service
{
    public class UserService : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ResponseModel<string>> Login(LoginModel loginModel)
        {
            var response = new ResponseModel<string>();
            var user = await _unitOfWork.userRepo.Login(loginModel.Email, loginModel.Password);
            if (user == null || user.IsDeleted)
            {
                response.Errors = "User not exsits or has been banned.";
                return response;
            }
            response.Data = "Login successful.";
            response.Role = new UserwithRole()

            {
                Role = user.Role.ToString(),

            };
            response.UserView = new UserView()
            {
                Id = user.Id,
                Address = user.Address,
                Email = user.Email,
                Name = user.Name,
                Phone = user.Phone
            };
            return response;
        }
        private async Task<string> CreateWallet()
        {
            var wallet = new Wallet()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Ví Tiền",
                Balance = 100000,
                BalanceHistory = 100000,
                CreationDate = DateTime.Now,
                IsDeleted = false
            };
            await _unitOfWork.walletRepo.CreateAsync(wallet);
            var isSuccess = await _unitOfWork.SaveChangeAsync();
            if (isSuccess > 0)
            {
                return wallet.Id;
            }
            return "fail";
        }
        public async Task<ResponseModel<string>> CreateUser(UserModel loginModel)
        {
            var isExist = await _unitOfWork.userRepo.ExistEmail(loginModel.Email);
            var response = new ResponseModel<string>();
            if (!isExist)
            {
                //var user = _mapper.Map<User>(loginModel);
                User user = new User() {
                    Id = (loginModel.Id.IsNullOrEmpty())? Guid.NewGuid().ToString(): loginModel.Id, 
                    Address = loginModel.Address,
                    Email = loginModel.Email,
                    CreationDate = DateTime.Now,
                    Name = loginModel.Name,
                    Password = loginModel.Password,
                    Phone = loginModel.Phone,
                    IsDeleted = false
                };
                string walletResult = await CreateWallet();
                user.Role = Role.Customer;
                if (!walletResult.Equals("fail"))
                    //user.WalletId = await _unitOfWork.userRepo.AutoIncreamentId();
                user.WalletId = walletResult;
                await _unitOfWork.userRepo.CreateAsync(user);
                var isSuccess = await _unitOfWork.SaveChangeAsync();
                if (isSuccess > 0)
                {
                    response.Role = new UserwithRole()
                    {
                        Role = user.Role.ToString(),
                    };
                    response.Data = "Register successfully";
                    response.UserView = new UserView()
                    {
                        Id = user.Id,
                        Address = user.Address,
                        Email = user.Email,
                        Name = user.Name,
                        Phone = user.Phone
                    };
                    return response;
                }
            }
            return new ResponseModel<string> { Errors = "Fail to create user." };
        }
        public async Task<User> GetUserById(string id)
        {
            var user = await _unitOfWork.userRepo.GetEntityByIdAsync(id);
            return user;
        }
        public async Task<List<User>> GetAllUser()
        {
            List<User> users = (await _unitOfWork.userRepo.GetAllAsync()).ToList();
            List<User> result = new List<User>();
            foreach (var item in users)
            {
                if (item.IsDeleted == false)
                {
                    result.Add(item);
                }
            }
            return result;
        } 
        public async Task<bool> DeleteUser(string id)
        {
            User user = await _unitOfWork.userRepo.GetEntityByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            user.DeletionDate = DateTime.Now;
            user.IsDeleted = true;
            _unitOfWork.userRepo.DeleteAsync(user);
            int check = await _unitOfWork.SaveChangeAsync();
            return check > 0 ? true : false;
        }
        public async Task<bool> UpdateUser(UserModel req, string id)
        {
            User user = await _unitOfWork.userRepo.GetEntityByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            user.Name = req.Name;
            user.Address = req.Address;
            user.Phone = req.Phone;
            user.Email = req.Email;
            user.Password = req.Password;
            user.ModificationDate = DateTime.Now;
            _unitOfWork.userRepo.UpdateAsync(user);
            int check = await _unitOfWork.SaveChangeAsync();
            return check > 0 ? true : false;
        }
        public async Task<bool> CheckCourseUser(string userId, string courseId)
        {
            //check if user has bought course
            var user = await _unitOfWork.userRepo.GetEntityByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            var course = await _unitOfWork.courseRepo.GetEntityByIdAsync(courseId);
            if (course == null)
            {
                return false;
            }
            UserCourse courseUser = await _unitOfWork.userCourseRepo.SingleOrDefaultAsync(
                predicate: x => x.UserId == userId && x.CourseId == course.Id);
            if (courseUser.CourseId == course.Id && courseUser.UserId == user.Id)
            {
                return true;
            }
            return false;
        }
    }
}