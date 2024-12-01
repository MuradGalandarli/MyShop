using BusinessLayer.Service;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using AutoMapper;
using DataTransferObject.DtoEntity;
using FluentValidation;

namespace ApiLayer.Controllers.Admin
{
    [Authorize(Roles ="admin,user")]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteProductController : ControllerBase
    {
        private readonly IFavoriteProductService _favoriteProduct;
        private readonly IMapper _mapper;
        private readonly IValidator<FavoriteProductDto> _validator;
        public FavoriteProductController(IFavoriteProductService favoriteProduct,
            IMapper mapper,
            IValidator<FavoriteProductDto> validator)
        {
            _favoriteProduct = favoriteProduct;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost("GetAllFavoriteProduct")]
        public async Task<IActionResult> GetAllFavoriteProduct()
        {
            var result = await _favoriteProduct.GetAll();
            return (result != null ? Ok(result) : BadRequest());
        }

        [HttpGet("GetByIdFavoriteProduct")]
        public async Task<IActionResult> GetByIdFavoriteProduct(int id)
        {
            var result = await _favoriteProduct.GetById(id);
            return (result != null ? Ok(result) : BadRequest());
        }

        [HttpPost("AddFavoriteProduct")]
        public async Task<IActionResult> AddCategory(FavoriteProductDto t)
        {
            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                var convertFavoriteProduct = _mapper.Map<FavoriteProduct>(t);
                bool IsSuccess = await _favoriteProduct.Add(convertFavoriteProduct);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateCategory(FavoriteProductDto t)
        {
            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                var convertFavoriteProduct = _mapper.Map<FavoriteProduct>(t);
                bool IsSuccess = await _favoriteProduct.Update(convertFavoriteProduct);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpDelete("DeleteFavoriteProduct")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool IsSuccess = await _favoriteProduct.Delete(id);
            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }
        
        [HttpGet("GetUserIdAllFavoriteProduct")]
        public async Task<IActionResult> GetUserIdAllFavoriteProduct(int userId)
        {

            var result = await _favoriteProduct.GetUserIdAllFavoriteProduct(userId);
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
