using System;
using System.Collections.Generic;

namespace Beauty_Art.Domains.Models
{
    public partial class UserAction
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? InsDate { get; set; }
        public DateTime? UpsDate { get; set; }
        public int? ActionValue { get; set; }
        public Guid? UserWalletId { get; set; }
        public string? Status { get; set; }

        public virtual UserWallet? UserWallet { get; set; }
    }
}
