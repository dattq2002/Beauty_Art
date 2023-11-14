using Beauty_Art.Domain.Paginate;
using Beauty_Art.Domains.Models;
using Beauty_Art.Payload.Request;

namespace Beauty_Art.Service.Interface
{
    public interface ICourseService
    {
        Task<IPaginate<Course>> GetListCourse(int page, int size);
        Task<Course> CreateCourse(CourseRequest req);
        Task<bool> DeleteCourse(Guid id);
        Task<Course> UpdateCourse(CourseRequest req, Guid id);
        Task<Course> GetCourseById(Guid id);
    }
}
