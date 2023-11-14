using Beauty_Art.Domain.Paginate;
using Beauty_Art.Domains.Models;
using Beauty_Art.Payload.Request;

namespace Beauty_Art.Service.Interface
{
    public interface IInstructorService
    {
        Task<Instructor> CreateInstructor(InstructorRequest req);
        Task<bool> DeleteInstructor(Guid id);
        Task<IPaginate<Instructor>> getList(int page, int size);
        Task<Instructor> UpdateInstructor(InstructorRequest req, Guid id);
        Task<Instructor> getInstructorById(Guid id);
    }
}
