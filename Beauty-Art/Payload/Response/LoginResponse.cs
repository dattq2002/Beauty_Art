using Pos_System.API.Enums;

namespace Beauty_Art.Payload.Response
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public Guid Account_Id { get; set; }
        public string Username { get; set; }
        public RoleEnum Role { get; set; }
    }
}
