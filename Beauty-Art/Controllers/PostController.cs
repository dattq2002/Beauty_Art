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
    public class PostController : BaseController<PostController>
    {
        private readonly IPostService _postService;

        public PostController(IPostService service, ILogger<PostController> logger) : base(logger)
        {
            _postService = service;
        }
        [HttpGet("get-all-post")]
        [ProducesResponseType(typeof(IPaginate<Post>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPost(int page, int size)
        {
            var listPost = await _postService.GetListPost(page, size);
            return Ok(listPost);
        }
        [HttpPost("create-post")]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreatePost([FromBody]PostRequest req)
        {
            var post = await _postService.CreatePost(req);
            if (post == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Post.CreatePostFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(post);
        }
        [HttpDelete("delete-post/{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var result = await _postService.DeletePost(id);
            if (!result)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Post.DeletePostFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(result);
        }
        [HttpGet("get-post-by-id/{id}")]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await _postService.GetPostById(id);
            if (post == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Post.GetPostFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(post);
        }
        [HttpPut("update-post/{id}")]
        [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePost([FromBody]PostRequest req, Guid id)
        {
            var post = await _postService.UpdatePost(req, id);
            if (post == null)
            {
                return BadRequest(new ErrorResponse()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = MessageConstant.Post.UpdatePostFailMessage,
                    TimeStamp = DateTime.Now
                });
            }
            return Ok(post);
        }

    }
}
