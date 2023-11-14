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
    public class InstructorController : BaseController<InstructorController>
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService service,ILogger<InstructorController> logger) : base(logger)
        {
            _instructorService = service;
        }
        [HttpGet("get-all-instructor")]
        [ProducesResponseType(typeof(IPaginate<Instructor>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllInstructor(int page, int size)
        {
            var listInstructor = await _instructorService.getList(page, size);
            return Ok(listInstructor);
        }
        [HttpPost("create-instructor")]
        [ProducesResponseType(typeof(Instructor), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateInstructor([FromBody]InstructorRequest req)
        {
            var instructor = await _instructorService.CreateInstructor(req);
            if (instructor == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Instructor.CreateInstructorFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(instructor);
        }
        [HttpDelete("delete-instructor")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteInstructor([FromQuery]Guid id)
        {
            var result = await _instructorService.DeleteInstructor(id);
            if (!result)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Instructor.DeleteInstructorFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(result);
        }
        [HttpPut("update-instructor")]
        [ProducesResponseType(typeof(Instructor), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateInstructor([FromBody]InstructorRequest req, [FromQuery]Guid id)
        {
            var instructor = await _instructorService.UpdateInstructor(req, id);
            if (instructor == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Instructor.UpdateInstructorFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(instructor);
        }
        [HttpGet("get-instructor/{id}")]
        [ProducesResponseType(typeof(Instructor), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInstructorById(Guid id)
        {
            var instructor = await _instructorService.getInstructorById(id);
            if (instructor == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Instructor.GetInstructorFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(instructor);
        }
    }
}
