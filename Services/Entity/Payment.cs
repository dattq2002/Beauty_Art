using Services.Enum;

namespace Services.Entity
{
    public class Payment : BaseEntity
    {
        public string Name { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public decimal Balance { get; set; }
        public PaymentStatus Status { get; set; }
        public Order Order { get; set; }
    }
}
