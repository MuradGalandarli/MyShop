using BusinessLayer.Service;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using SahredLayer;

namespace ApiLayer.Controllers.UI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UIProductController : ControllerBase
    {
        private readonly IProductService _product;
        public UIProductController(IProductService productService)
        {
            _product = productService;
        }

        [HttpPost("AllProductUI")]
        public async Task<IActionResult> AllProduct()
        {
            var result = await _product.GetAll();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(result, options);
           
            return (json != null ? Ok(json) : BadRequest());
        }

        [HttpGet("GetByProductUI({id})")]
        public async Task<IActionResult> GetIdProduct(int id)
        {
            var result = await _product.GetById(id);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(result, options);
                
            return (json != null ? Ok(json) : BadRequest());
        }
        [HttpPost("SearchProductUI")]
        public async Task<IActionResult> SearchProduct(SearchProductModel product)
        {
            var result = await _product.SearchProduct(product);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(result, options);

            return (json != null ? Ok(json) : BadRequest());
        }

        [HttpGet("MostRecentlyUploaded")]
        public async Task<IActionResult> MostRecentlyUploaded()
        {
            var result = await _product.MostRecentlyUploaded();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(result, options);

            return (json != null ? Ok(json) : BadRequest());
        }

        [HttpGet("GetByIdProductUI/{id}")]
        public async Task<IActionResult> GetByIdProductUI(int id)
            {
            var result = await _product.GetByIdProductUI(id);
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
