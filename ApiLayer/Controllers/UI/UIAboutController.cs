using AutoMapper;
using BusinessLayer.Service;
using DataTransferObject.ResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLayer.Controllers.UI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UIAboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;
        private readonly IMapper _mapper;
        public UIAboutController(IAboutService aboutService,
            IMapper mapper)
        {
            _aboutService = aboutService;
            _mapper = mapper;
        }

        [HttpGet("GetAllListAboutIsActiveUI")]
        public async Task<IActionResult> GetListAllIsActiveUI()
        {
            var result = await _aboutService.GetListAllIsActiveUI();
            var mapAbout = _mapper.Map<ResponseAbout>(result);
            return mapAbout != null ? Ok(mapAbout) : BadRequest();
         
            
        }


    }
}
