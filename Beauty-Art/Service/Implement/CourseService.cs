using AutoMapper;
using Beauty_Art.API.Services;
using Beauty_Art.API.Utils;
using Beauty_Art.Domain.Paginate;
using Beauty_Art.Domains.Models;
using Beauty_Art.Enums;
using Beauty_Art.Payload.Request;
using Beauty_Art.Repository.Interfaces;
using Beauty_Art.Service.Interface;

namespace Beauty_Art.Service.Implement
{
    public class CourseService : BaseService<CourseService>, ICourseService
    {
        public CourseService(IUnitOfWork<BEAUTIFUL_ARTSContext> unitOfWork, ILogger<CourseService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<IPaginate<Course>> GetListCourse(int page, int size)
        {
            IPaginate<Course> listCourse = (IPaginate<Course>)await _unitOfWork.GetRepository<Course>()
                .GetPagingListAsync(
                predicate: x => x.Status == CourseStatusEnum.CourseStatus.CON_HANG.GetDescriptionFromEnum() &&!x.Deflag,              
                page: page, size: size);
            return listCourse;
        }

        public async Task<Course> CreateCourse(CourseRequest req)
        {
            Course course = new Course()
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Price = req.Price,
                Status = CourseStatusEnum.CourseStatus.CON_HANG.GetDescriptionFromEnum(),
                Deflag = false,
                InsDate = DateTime.Now,
                UpsDate = DateTime.Now,
                Description = req.Description   
            };
            await _unitOfWork.GetRepository<Course>().InsertAsync(course);
            await _unitOfWork.CommitAsync();
            return course;
        }

        public async Task<bool> DeleteCourse(Guid id)
        {
            Course course = await _unitOfWork.GetRepository<Course>().SingleOrDefaultAsync(
                predicate: x => !x.Deflag && x.Id.Equals(id)
                );
            if (course == null)
            {
                return false;
            }
            course.Deflag = true;
            _unitOfWork.GetRepository<Course>().UpdateAsync(course);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<Course> UpdateCourse(CourseRequest req, Guid id)
        {
            Course course = await _unitOfWork.GetRepository<Course>().SingleOrDefaultAsync(
                               predicate: x => !x.Deflag && x.Id.Equals(id)
                                              );
            if (course == null)
            {
                return null;
            }
            course.Name = req.Name;
            course.Price = req.Price;
            course.Description = req.Description;
            course.UpsDate = DateTime.Now;
            course.Status = req.Status;
            _unitOfWork.GetRepository<Course>().UpdateAsync(course);
            await _unitOfWork.CommitAsync();
            return course;
        }
        public async Task<Course> GetCourseById(Guid id)
        {
            Course course = await _unitOfWork.GetRepository<Course>().SingleOrDefaultAsync(
                               predicate: x => !x.Deflag && x.Id.Equals(id)
                                              );
            if (course == null)
            {
                return null;
            }
            return course;
        }
    }
}
