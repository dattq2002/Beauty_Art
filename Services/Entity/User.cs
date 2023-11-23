using Services.Enum;

namespace Services.Entity
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Role Role { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
        public string WalletId { get; set; }
        public Wallet Wallet { get; set; }
    }
}
