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
using BusinessLayer.Helpers;
using DataTransferObject.ResponseDto;

namespace ApiLayer.Controllers.Admin
{
    [Authorize(Roles ="Admin,User")]
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
            if (result != null)
            {
                var mapFavoriteProduct = _mapper.Map<List<ResponseFavoriteProduct>>(result);
                return Ok(mapFavoriteProduct);

            }
            return BadRequest();
        }

        [HttpGet("GetByIdFavoriteProduct/{id}")]
        public async Task<IActionResult> GetByIdFavoriteProduct(int id)
        {
            var result = await _favoriteProduct.GetById(id); if (result != null)
            {
                var mapFavoriteProduct = _mapper.Map<ResponseFavoriteProduct>(result);
                return Ok(mapFavoriteProduct);

            }
            return BadRequest();
        }

        [HttpPost("AddFavoriteProduct")]
        public async Task<IActionResult> AddCategory(FavoriteProductDto t)
        {
            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                string token = Request.Headers["Authentication"];
                string userId = TokenHelper.ProcessToken(token);
                var convertFavoriteProduct = _mapper.Map<FavoriteProduct>(t);
                convertFavoriteProduct.UserIdFromToken = userId;
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
                string token = Request.Headers["Authentication"];
                string userId = TokenHelper.ProcessToken(token);
                var convertFavoriteProduct = _mapper.Map<FavoriteProduct>(t);
                convertFavoriteProduct.UserIdFromToken = userId;    
                bool IsSuccess = await _favoriteProduct.Update(convertFavoriteProduct);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpDelete("DeleteFavoriteProduct/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool IsSuccess = await _favoriteProduct.Delete(id);
            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }
        
        [HttpGet("GetUserIdAllFavoriteProduct")]
        public async Task<IActionResult> GetUserIdAllFavoriteProduct()
        {
            string token = Request.Headers["Authentication"];
            string userId = TokenHelper.ProcessToken(token);
            var result = await _favoriteProduct.GetUserIdAllFavoriteProduct(userId);
            var mapFavoriteProduct = _mapper.Map<List<ResponseProduct>>(result);
           
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(mapFavoriteProduct, options);

            return (json != null ? Ok(json) : BadRequest());
          
        }



    }
}
