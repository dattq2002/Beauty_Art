namespace Beauty_Art.Payload.Request
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Confirm_Password { get;set;}
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
    }
}
