using AutoMapper;
using Beauty_Art.API.Services;
using Beauty_Art.Domain.Paginate;
using Beauty_Art.Domains.Models;
using Beauty_Art.Payload.Request;
using Beauty_Art.Repository.Interfaces;
using Beauty_Art.Service.Interface;

namespace Beauty_Art.Service.Implement
{
    public class InstructorService : BaseService<InstructorService>, IInstructorService
    {
        public InstructorService(IUnitOfWork<BEAUTIFUL_ARTSContext> unitOfWork, ILogger<InstructorService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<Instructor> CreateInstructor(InstructorRequest req)
        {
            Instructor instructor = new Instructor()
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                PhoneNumber = req.Phone,
                DateOfBirth = req.Birthday,
                Email = req.Email,
                Deflag = false,
                InsDate = DateTime.Now,
                UpsDate = DateTime.Now,
                ImageUrl = req.Image_url
            };
            await _unitOfWork.GetRepository<Instructor>().InsertAsync(instructor);
            await _unitOfWork.CommitAsync();
            return instructor;
        }
        public async Task<bool> DeleteInstructor(Guid id)
        {
            Instructor instructor = await _unitOfWork.GetRepository<Instructor>().SingleOrDefaultAsync(
                               predicate: x => !x.Deflag && x.Id.Equals(id)
                                              );
            if (instructor == null)
            {
                return false;
            }
            instructor.Deflag = true;
            _unitOfWork.GetRepository<Instructor>().UpdateAsync(instructor);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<IPaginate<Instructor>> getList(int page, int size)
        {
            IPaginate<Instructor> listInstructor = (IPaginate<Instructor>)await _unitOfWork.GetRepository<Instructor>()
                .GetPagingListAsync(
                               predicate: x => !x.Deflag,
                                              page: page, size: size);
            return listInstructor;
        }
        public  async Task<Instructor> UpdateInstructor(InstructorRequest req, Guid id)
        {
            Instructor instructor = await _unitOfWork.GetRepository<Instructor>().SingleOrDefaultAsync(
                                              predicate: x => !x.Deflag && x.Id.Equals(id)
                                                                                           );
            if (instructor == null)
            {
                return null;
            }
            instructor.Name = req.Name;
            instructor.PhoneNumber = req.Phone;
            instructor.DateOfBirth = req.Birthday;
            instructor.Email = req.Email;
            instructor.Deflag = false;
            instructor.UpsDate = DateTime.Now;
            instructor.ImageUrl = req.Image_url;
            _unitOfWork.GetRepository<Instructor>().UpdateAsync(instructor);
            await _unitOfWork.CommitAsync();
            return instructor;
        }
        public async Task<Instructor> getInstructorById(Guid id)
        {
            Instructor instructor = await _unitOfWork.GetRepository<Instructor>().SingleOrDefaultAsync(
                                                             predicate: x => !x.Deflag && x.Id.Equals(id)
                                                                                                                                                       );
            if (instructor == null)
            {
                return null;
            }
            return instructor;
        }
    }
}
