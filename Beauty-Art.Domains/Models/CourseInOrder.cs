using System;
using System.Collections.Generic;

namespace Beauty_Art.Domains.Models
{
    public partial class CourseInOrder
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? CourseId { get; set; }
        public DateTime? InsDate { get; set; }
        public DateTime? UpsDate { get; set; }
        public bool? Deflag { get; set; }
        public string? Status { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Order? Order { get; set; }
    }
}
