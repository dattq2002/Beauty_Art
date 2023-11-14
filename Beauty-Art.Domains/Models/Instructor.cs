using System;
using System.Collections.Generic;

namespace Beauty_Art.Domains.Models
{
    public partial class Instructor
    {
        public Instructor()
        {
            InstructorInCourses = new HashSet<InstructorInCourse>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public string? Email { get; set; }
        public DateTime InsDate { get; set; }
        public DateTime UpsDate { get; set; }
        public bool Deflag { get; set; }

        public virtual ICollection<InstructorInCourse> InstructorInCourses { get; set; }
    }
}
