using System;
using System.Collections.Generic;

namespace Beauty_Art.Domains.Models
{
    public partial class Order
    {
        public Order()
        {
            CourseInOrders = new HashSet<CourseInOrder>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public DateTime? InsDate { get; set; }
        public DateTime? UpsDate { get; set; }
        public bool? Deflag { get; set; }
        public string? Status { get; set; }
        public Guid? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<CourseInOrder> CourseInOrders { get; set; }
    }
}
