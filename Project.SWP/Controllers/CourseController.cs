using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Model;
using Services.Service;
using Services.Service.Interface;

namespace Project.SWP.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseServices _courseServices;
        public CourseController(ICourseServices services)
        {
            _courseServices = services;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseModel courseModel)
        {
            var result = await _courseServices.CreateCourse(courseModel);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCourse()
        {
            var result = await _courseServices.GetCoursesAsync();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCourseById([FromQuery]string id)
        {
            var result = await _courseServices.GetCourseById(id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update( [FromBody] CourseModel courseModel,[FromQuery] string id)
        {
            var result = await _courseServices.UpdateCourse(courseModel,id);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var result = await _courseServices.DeleteCourse(id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCourseStatus([FromQuery] string id)
        {
            var result = await _courseServices.UpdateCourseStatus(id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUnPublishCourse([FromQuery] string id)
        {
            var result = await _courseServices.UpdateUnPublishCourse(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCourseByUserId([FromQuery] string userId)
        {
            var result = await _courseServices.GetCourseByUserId(userId);
            return Ok(result);
        }
    }
}
