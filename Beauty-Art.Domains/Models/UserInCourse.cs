using System;
using System.Collections.Generic;

namespace Beauty_Art.Domains.Models
{
    public partial class UserInCourse
    {
        public Guid Id { get; set; }
        public Guid? CourseId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? AttendDate { get; set; }
        public string? Status { get; set; }
        public DateTime? ExpireDate { get; set; }

        public virtual Course? Course { get; set; }
        public virtual User? User { get; set; }
    }
}
