namespace Services.Entity
{
    public class UserCourse : BaseEntity
    {
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
