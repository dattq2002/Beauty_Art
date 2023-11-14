using System;
using System.Collections.Generic;

namespace Beauty_Art.Domains.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            Posts = new HashSet<Post>();
            UserInCourses = new HashSet<UserInCourse>();
            UserWallets = new HashSet<UserWallet>();
        }

        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? InsDate { get; set; }
        public DateTime? UpsDate { get; set; }
        public bool Deflag { get; set; }
        public Guid AccountId { get; set; }
        public string? ImageUrl { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<UserInCourse> UserInCourses { get; set; }
        public virtual ICollection<UserWallet> UserWallets { get; set; }
    }
}
