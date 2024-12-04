using BusinessLayer.Service;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using AutoMapper;
using DataTransferObject.ResponseDto;

namespace ApiLayer.Controllers.UI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UICategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public UICategoryController(ICategoryService categoryService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;   
        }

        [HttpPost("GetAllCategoryUI")]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _categoryService.GetAll();
          
            var mapCategory = _mapper.Map<List<ResponseCategory>>(result);

            return mapCategory != null ? Ok(mapCategory) : BadRequest();

        }

        [HttpPost("GetByIdAllProductUI")]
        public async Task<IActionResult> GetByIdAllProduct(int id)
        {
          
            var result = await _categoryService.GetCategoryIdAllProduct(id);
            var mapCategory = _mapper.Map<List<ResponseCategory>>(result);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var category = JsonSerializer.Serialize(mapCategory, options);
           
            return category != null ? Ok(category) : BadRequest();
        }




    }
}
