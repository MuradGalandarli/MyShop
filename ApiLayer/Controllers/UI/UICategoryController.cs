using BusinessLayer.Service;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ApiLayer.Controllers.UI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UICategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public UICategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("GetAllCategoryUI")]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _categoryService.GetAll();
            return (result != null ? Ok(result) : BadRequest());
        }

        [HttpPost("GetByIdAllProductUI")]
        public async Task<IActionResult> GetByIdAllProduct(int id)
        {
          
            var result = await _categoryService.GetCategoryIdAllProduct(id);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(result, options);

            return (json != null ? Ok(json) : BadRequest());
        }




    }
}
