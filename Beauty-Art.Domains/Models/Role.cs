using System;
using System.Collections.Generic;

namespace Beauty_Art.Domains.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime InsDate { get; set; }
        public DateTime UpsDate { get; set; }
        public bool Deflag { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
