using Services.Commons;
using Services.Entity;

namespace Services.Repository.Interface
{
    public interface ICourseRepo : IGenericRepo<Course>
    {
        Task<string> AutoIncreamentId();
    }
}
