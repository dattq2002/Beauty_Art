using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Model;
using Services.Service.Interface;

namespace Project.SWP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterService _chapterService;

        public ChapterController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChapterModel chapterModel)
        {
            var result = await _chapterService.CreateChapter(chapterModel);
            return Ok(result);
        }
        [HttpGet("GetAllChapters")]
        public async Task<IActionResult> GetAllChapter()
        {
            var result = await _chapterService.GetChaptersAsync();
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetChapterById([FromQuery] string id)
        {
            var result = await _chapterService.GetChapterById(id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ChapterModel chapterModel, [FromQuery] string id)
        {
            var result = await _chapterService.UpdateChapter(chapterModel, id);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var result = await _chapterService.DeleteChapter(id);
            return Ok(result);
        }
    }
}
