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
    [Authorize(Roles ="admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductDto> _validator;
        public ProductController(IProductService productService,
            IMapper mapper,
            IValidator<ProductDto> validator)
        {
            _productService = productService;
            _mapper = mapper;
            _validator = validator;
        }
/*
        [HttpPost("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var result = await _productService.GetAll();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(result, options);

            return (json != null ? Ok(json) : BadRequest());
        }*/

       /* [HttpGet("GetByIdProduct/{id}")]
        public async Task<IActionResult> GetByIdProduct(int id)
        {
            var result = await _productService.GetById(id);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(result, options);

            return (json != null ? Ok(json) : BadRequest());
        }*/

        [HttpPost("AddProductWishImage")]
        public async Task<IActionResult> AddProductWishImage([FromForm] ProductDto t)
        {
            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                var convertProduct = _mapper.Map<Product>(t);
                bool IsSuccess = await _productService.Add(convertProduct);

                /* var options = new JsonSerializerOptions
                 {
                     ReferenceHandler = ReferenceHandler.Preserve,
                     WriteIndented = true
                 };

                 var json = JsonSerializer.Serialize(result, options);*/
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpPut("UpdateProductWishImage")]
        public async Task<IActionResult> UpdateProductWishImage([FromForm] ProductDto t)
        {
            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                var convertProduct = _mapper.Map<Product>(t);
                bool IsSuccess = await _productService.Update(convertProduct);
                /*
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                var json = JsonSerializer.Serialize(result, options);*/
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpDelete("DeleteProduct({id})")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            bool IsSuccess = await _productService.Delete(id);
            /*  var options = new JsonSerializerOptions
              {
                  ReferenceHandler = ReferenceHandler.Preserve,
                  WriteIndented = true
              };

              var json = JsonSerializer.Serialize(result, options);*/

            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }

    }
}
