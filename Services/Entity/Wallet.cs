using Services.Enum;

namespace Services.Entity
{
    public class Wallet : BaseEntity
    {
        public string Name { get; set;}
        public int Balance { get; set;}
        public int BalanceHistory { get; set;}
        public Status Status { get; set;}
        public User User { get; set; }
    }
}
