using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Services.Commons;
using Services.Entity;
using Services.Model;

namespace Services.Service
{
    public class CourseServices : ICourseServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Course> CreateCourse(CourseModel courseModel)
        {
            var course = new Course()
            {
                Id = courseModel.Id.IsNullOrEmpty() ? Guid.NewGuid().ToString() : courseModel.Id,
                title = courseModel.Title,
                CourseDescription = courseModel.CourseDescription,
                CreationDate = DateTime.Now,
                Price = (decimal)courseModel.Price,
                imageUrl = courseModel.imageUrl,
                IsPulished = courseModel.isPublish,
                IsDeleted = false,
                CategoryId = courseModel.CategoryId
            };
            //course.CategoryId = await _unitOfWork.courseRepo.AutoIncreamentId();
            await _unitOfWork.courseRepo.CreateAsync(course);
            var isSuccess = await _unitOfWork.SaveChangeAsync();
            if (isSuccess > 0)
            {
                return course;
            }
            return null;
        }
        public async Task<bool> UpdateCourse(CourseModel courseModel, string id)
        {
            var course = await _unitOfWork.courseRepo.GetEntityByIdAsync(id);
            if (course == null)
            {
                return false;
            }
            course.title = courseModel.Title;
            course.CourseDescription = courseModel.CourseDescription;
            course.Price = (decimal)courseModel.Price;
            course.imageUrl = courseModel.imageUrl;
            course.IsPulished = courseModel.isPublish;
            _unitOfWork.courseRepo.UpdateAsync(course);
            var isSuccess = await _unitOfWork.SaveChangeAsync();
            if (isSuccess > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateCourseStatus(string id)
        {
            var course = await _unitOfWork.courseRepo.GetEntityByIdAsync(id);
            if (course == null)
            {
                return false;
            }
            course.IsPulished = true;
            _unitOfWork.courseRepo.UpdateAsync(course);
            var isSuccess = await _unitOfWork.SaveChangeAsync();
            if (isSuccess > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateUnPublishCourse(string id)
        {
            var course = await _unitOfWork.courseRepo.GetEntityByIdAsync(id);
            if (course == null)
            {
                return false;
            }
            course.IsPulished = false;
            _unitOfWork.courseRepo.UpdateAsync(course);
            var isSuccess = await _unitOfWork.SaveChangeAsync();
            if (isSuccess > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> DeleteCourse(string id)
        {
            Course course = await _unitOfWork.courseRepo.GetEntityByIdAsync(id);
            if (course == null)
            {
                return false;
            }
            course.DeletionDate = DateTime.Now;
            course.IsDeleted = true;
            _unitOfWork.courseRepo.DeleteAsync(course);
            int check = await _unitOfWork.SaveChangeAsync();
            return check > 0 ? true : false;
        }
        public async Task<CourseResponse> GetCourseById(string id)
        {
            Course course = await _unitOfWork.courseRepo.GetEntityByIdAsync(id);
            List<Chapter> chapters = (List<Chapter>) await _unitOfWork.chapterRepo.GetListAsync(x => x.CourseId.Equals(id));
            Category category = await _unitOfWork.categoryRepo.GetEntityByIdAsync(course.CategoryId);
            CourseResponse response = new CourseResponse()
            {
                Id = course.Id,
                title = course.title,
                CourseDescription = course.CourseDescription,
                Price = course.Price,
                imageUrl = course.imageUrl,
                IsPulished = course.IsPulished,
                CategoryId = course.CategoryId,
                Category = category,
                chapters = chapters
            };
            return response;
        }
        public async Task<List<Course>> GetCoursesAsync()
        {
            List<Course> courses = (await _unitOfWork.courseRepo.GetAllAsync()).ToList();
            List<Course> result = new List<Course>();
            List<Chapter> chapters = new List<Chapter>();
            foreach (var item in courses)
            {
                if (item.IsDeleted == false)
                {
                    Category category = await _unitOfWork.categoryRepo.GetEntityByIdAsync(item.CategoryId);
                    item.Category = category;
                    //add list chapter to course
                    List<Chapter> chapterss = (List<Chapter>)await _unitOfWork.chapterRepo.GetListAsync(x => x.CourseId.Equals(item.Id));
                    foreach (var chapter in chapterss)
                    {
                        if (chapter.CourseId == item.Id)
                        {
                            if(chapter.IsDeleted == false)
                            {
                                chapters.Add(chapter);
                            }
                            item.Chapters = chapters;
                        }
                    }
                    
                    result.Add(item);
                }
            }
            return result;
        }
        public async Task<List<Course>> GetCourseByUserId(string userId)
        {
            List<UserCourse> userCourses = (List<UserCourse>)await _unitOfWork.userCourseRepo.GetListAsync(x => x.UserId.Equals(userId));
            List<Course> courses = new List<Course>();
            foreach (var item in userCourses)
            {
                Course course = await _unitOfWork.courseRepo.GetEntityByIdAsync(item.CourseId);
                course.Chapters = (List<Chapter>)await _unitOfWork.chapterRepo.GetListAsync(x => x.CourseId.Equals(course.Id));
                if (course.IsDeleted == false)
                {
                    courses.Add(course);
                }
            }
            return courses;
        }
    } 
}
