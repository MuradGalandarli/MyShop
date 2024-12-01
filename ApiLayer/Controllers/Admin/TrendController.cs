using BusinessLayer.Service;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;

namespace ApiLayer.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrendController : ControllerBase
    {
        private readonly ITrendService _trendService;
        public TrendController(ITrendService _trendService)
        {
           this._trendService = _trendService;
        }
        
        [HttpGet("GetMostClicked")]
        public async Task<IActionResult> GetMostClicked()
        {
            var result = await _trendService.GetMostClicked();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(result, options);

          
            return json != null ? Ok(json) : BadRequest();
        }

        [HttpGet("TrendProduct")]
        public async Task<IActionResult> Trend()
        {
            var result = await _trendService.Trend();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(result, options);

            return (json != null ? Ok(json) : BadRequest());
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteTrendProduct/{productId}")]
        public async Task<IActionResult> DeleteTrendProduct(int productId)
        {
           await _trendService.DeleteProductFromTrend(productId);
            return Ok();
        }




    }
}
