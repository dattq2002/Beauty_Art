using Services.Commons;
using Services.Entity;
using Services.Model;

namespace Services.Service
{
    public interface ICourseServices
    {
        Task<Course> CreateCourse(CourseModel courseModel);
        Task<bool> UpdateCourse(CourseModel courseModel, string id);
        Task<bool> DeleteCourse(string id);
        Task<CourseResponse> GetCourseById(string id);
        Task<List<Course>> GetCoursesAsync();
        Task<bool> UpdateCourseStatus(string id);
        Task<bool> UpdateUnPublishCourse(string id);
        Task<List<Course>> GetCourseByUserId(string userId);
    }
}
