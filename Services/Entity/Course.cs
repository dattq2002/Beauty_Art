using Services.Enum;

namespace Services.Entity
{
    public class Course : BaseEntity
    {
        public string title { get; set; }
        public string CourseDescription { get; set; }
        public decimal Price { get; set; }
        public string imageUrl { get; set; }
        public Boolean IsPulished { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Chapter>? Chapters { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }


    }
}
