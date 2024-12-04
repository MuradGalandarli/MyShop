using BusinessLayer.Service;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using DataTransferObject.ResponseDto;
using AutoMapper;

namespace ApiLayer.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrendController : ControllerBase
    {
        private readonly ITrendService _trendService;
        private readonly IMapper _mapper;
        public TrendController(ITrendService _trendService,
             IMapper mapper)
        {
            this._trendService = _trendService;
            _mapper = mapper;
        }

        [HttpGet("GetMostClicked")]
        public async Task<IActionResult> GetMostClicked()
        {
            var result = await _trendService.GetMostClicked();
            var mapTrend = _mapper.Map<List<ResponseProduct>>(result);
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
           
            var mostClicked = JsonSerializer.Serialize(mapTrend, options);

            return mostClicked != null ? Ok(mostClicked) : BadRequest();

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

            var trend = JsonSerializer.Serialize(result, options);

            var mapTrend = _mapper.Map<List<ResponseProduct>>(trend);
            return mapTrend != null ? Ok(mapTrend) : BadRequest();


        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteTrendProduct/{productId}")]
        public async Task<IActionResult> DeleteTrendProduct(int productId)
        {
            await _trendService.DeleteProductFromTrend(productId);
            return Ok();
        }




    }
}
