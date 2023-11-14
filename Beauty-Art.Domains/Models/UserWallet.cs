using System;
using System.Collections.Generic;

namespace Beauty_Art.Domains.Models
{
    public partial class UserWallet
    {
        public UserWallet()
        {
            UserActions = new HashSet<UserAction>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int? Balance { get; set; }
        public int? BalanceHistory { get; set; }
        public DateTime? InsDate { get; set; }
        public DateTime? UpsDate { get; set; }
        public bool? DeFlag { get; set; }
        public Guid? WalletTypeId { get; set; }
        public Guid? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual WalletType? WalletType { get; set; }
        public virtual ICollection<UserAction> UserActions { get; set; }
    }
}
