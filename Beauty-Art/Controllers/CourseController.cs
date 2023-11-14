using Beauty_Art.API.Constants;
using Beauty_Art.API.Controllers;
using Beauty_Art.API.Payload.Response;
using Beauty_Art.Domain.Paginate;
using Beauty_Art.Domains.Models;
using Beauty_Art.Payload.Request;
using Beauty_Art.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Beauty_Art.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : BaseController<CourseController>
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService service, ILogger<CourseController> logger) : base(logger)
        {
            _courseService = service;
        }
        [HttpGet("get-all-course")]
        [ProducesResponseType(typeof(IPaginate<Course>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCourse(int page, int size)
        {
            var listCourse = await _courseService.GetListCourse(page, size);
            return Ok(listCourse);
        }
        [HttpPost("create-course")]
        [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateCourse([FromBody]CourseRequest req)
        {
            var course = await _courseService.CreateCourse(req);
            if (course == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Course.CreateCourseFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(course);
        }
        [HttpDelete("delete-course")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCourse([FromQuery]Guid id)
        {
            var result = await _courseService.DeleteCourse(id);
            if (!result)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Course.DeleteCourseFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(result);
        }
        [HttpPut("update-course")]
        [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCourse([FromBody]CourseRequest req, [FromQuery]Guid id)
        {
            var course = await _courseService.UpdateCourse(req, id);
            if (course == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Course.UpdateCourseFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(course);
        }
        [HttpGet("get-course/{id}")]
        [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            var course = await _courseService.GetCourseById(id);
            if (course == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Course.GetCourseFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(course);
        }
    }
}
