using System;
using System.Collections.Generic;

namespace Beauty_Art.Domains.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseInOrders = new HashSet<CourseInOrder>();
            InstructorInCourses = new HashSet<InstructorInCourse>();
            UserInCourses = new HashSet<UserInCourse>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public DateTime InsDate { get; set; }
        public DateTime UpsDate { get; set; }
        public bool Deflag { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<CourseInOrder> CourseInOrders { get; set; }
        public virtual ICollection<InstructorInCourse> InstructorInCourses { get; set; }
        public virtual ICollection<UserInCourse> UserInCourses { get; set; }
    }
}
