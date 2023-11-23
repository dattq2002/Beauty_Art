using Services.Commons;
using Services.Entity;
using Services.Model;

namespace Services.Service.Interface
{
    public interface IUserServices
    {
        Task<ResponseModel<string>> Login(LoginModel loginModel);
        Task<ResponseModel<string>> CreateUser(UserModel loginModel);
        Task<User> GetUserById(string id);
        Task<List<User>> GetAllUser();
        Task<bool> DeleteUser(string id);
        Task<bool> UpdateUser(UserModel req, string id);
        Task<bool> CheckCourseUser(string userId, string courseId);

    }
}
