using BusinessLayer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLayer.Controllers.UI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UIAboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;
        public UIAboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet("GetAllListAboutIsActiveUI")]
        public async Task<IActionResult> GetListAllIsActiveUI()
        {
            var result = await _aboutService.GetListAllIsActiveUI();
            return result != null ? Ok(result) : BadRequest();
        }


    }
}
