using Services.Enum;

namespace Services.Entity
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
