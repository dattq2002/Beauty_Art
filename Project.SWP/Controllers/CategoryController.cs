using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Model;
using Services.Service.Interface;

namespace Project.SWP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _services;
        public CategoryController(ICategoryService services)
        {
            _services = services;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryModel model)
        {
            var result = await _services.CreateCategory(model);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryModel model,[FromQuery] string id)
        {
            var result = await _services.UpdateCategory(model, id);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromQuery] string id)
        {
            var result = await _services.DeleteCategory(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCategoryById([FromQuery] string id)
        {
            var result = await _services.GetCategoryById(id);
            return Ok(result);
        }
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _services.GetCategoriesAsync();
            return Ok(result);
        }
    }
}
