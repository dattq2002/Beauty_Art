using System;
using System.Collections.Generic;

namespace Beauty_Art.Domains.Models
{
    public partial class WalletType
    {
        public WalletType()
        {
            UserWallets = new HashSet<UserWallet>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? InsDate { get; set; }
        public DateTime? UpsDate { get; set; }
        public bool? DeFlag { get; set; }

        public virtual ICollection<UserWallet> UserWallets { get; set; }
    }
}
