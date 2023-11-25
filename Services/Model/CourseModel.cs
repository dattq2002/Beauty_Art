using Services.Entity;
using Services.Enum;


namespace Services.Model
{
    public class CourseModel
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? CourseDescription { get; set; }
        public decimal? Price { get; set; }
        public string? imageUrl { get; set; }
        public bool isPublish { get; set; }
        public string CategoryId { get; set; }
    }
    public class CourseResponse
    {
        public string Id { get; set; }
        public string title { get; set; }
        public string CourseDescription { get; set; }
        public decimal Price { get; set; }
        public string imageUrl { get; set; }
        public Boolean IsPulished { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Chapter> chapters { get; set; }
    }
}
