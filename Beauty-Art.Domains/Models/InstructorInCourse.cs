using System;
using System.Collections.Generic;

namespace Beauty_Art.Domains.Models
{
    public partial class InstructorInCourse
    {
        public Guid Id { get; set; }
        public Guid? InstructorId { get; set; }
        public Guid? CourseId { get; set; }
        public bool? Deflag { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Instructor? Instructor { get; set; }
    }
}
