using BusinessLayer.Service;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using SahredLayer;
using DataTransferObject.ResponseDto;
using AutoMapper;

namespace ApiLayer.Controllers.UI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UIProductController : ControllerBase
    {
        private readonly IProductService _product;
        private readonly IMapper _mapper;
        public UIProductController(IProductService productService,
            IMapper mapper)
        {
            _product = productService;
            _mapper = mapper;
        }

        [HttpPost("AllProductUI")]
        public async Task<IActionResult> AllProduct()
        {
            var result = await _product.GetAll();
            var mapProduct = _mapper.Map<List<ResponseProduct>>(result);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            var data = JsonSerializer.Serialize(mapProduct, options);
            return data != null ? Ok(data) : BadRequest();
        }

        [HttpGet("GetByProductUI/{id}")]
        public async Task<IActionResult> GetIdProduct(int id)
        {
            var result = await _product.GetById(id);
            var mapProduct = _mapper.Map<ResponseAbout>(result);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            var data = JsonSerializer.Serialize(mapProduct, options);
            return data != null ? Ok(data) : BadRequest();
        }

        [HttpPost("SearchProductUI")]
        public async Task<IActionResult> SearchProduct(SearchProductModel product)
        {
            var result = await _product.SearchProduct(product);
            var mapProduct = _mapper.Map<List<ResponseProduct>>(result);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(mapProduct, options);

            return (json != null ? Ok(json) : BadRequest());
        }

        [HttpGet("MostRecentlyUploaded")]
        public async Task<IActionResult> MostRecentlyUploaded()
        {
            var result = await _product.MostRecentlyUploaded();
            var mapProduct = _mapper.Map<List<ResponseProduct>>(result);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            var data = JsonSerializer.Serialize(mapProduct, options);

            return data != null ? Ok(data) : BadRequest();

        }

        [HttpGet("GetByIdProductUI/{id}")]
        public async Task<IActionResult> GetByIdProductUI(int id)
        {
            var result = await _product.GetByIdProductUI(id);
            var mapProduct = _mapper.Map<ResponseProduct>(result);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            var data = JsonSerializer.Serialize(mapProduct, options);

            return data != null ? Ok(data) : BadRequest();

        }
    }
}