using AutoMapper;
using BusinessLayer.Service;
using DataTransferObject.DtoEntity;
using EntityLayer.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLayer.Controllers.Admin
{
    [Authorize (Roles ="admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;
        private readonly IValidator<CategoryDto> validator; 
        public CategoryController(ICategoryService categoryService,
            IMapper _mapper,
            ILogger<CategoryController> logger,
            IValidator<CategoryDto> validator)
        {
            _categoryService = categoryService;
            this._mapper = _mapper;
            _logger = logger;
            this.validator = validator;
        }

        [HttpPost("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _categoryService.GetAll();
            return (result != null ? Ok(result) : BadRequest());
        }

        [HttpGet("GetByIdCategory{id}")]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var result =  await _categoryService.GetById(id);
            return (result != null ? Ok(result) : BadRequest());
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody]CategoryDto t)
        {
            var resultValid = await validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                var convertCategory = _mapper.Map<Category>(t);
                bool IsSuccess = await _categoryService.Add(convertCategory);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryDto t)
        {
            var resultValid = await validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                var convertCategory = _mapper.Map<Category>(t);
                bool IsSuccess = await _categoryService.Update(convertCategory);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpDelete("DeleteCategory{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool IsSuccess = await _categoryService.Delete(id);
            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }

    }
}
