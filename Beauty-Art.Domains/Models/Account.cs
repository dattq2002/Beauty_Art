using System;
using System.Collections.Generic;

namespace Beauty_Art.Domains.Models
{
    public partial class Account
    {
        public Account()
        {
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid RoleId { get; set; }
        public DateTime InsDate { get; set; }
        public DateTime UpsDate { get; set; }
        public bool Deflag { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<User> Users { get; set; }
    }
}
