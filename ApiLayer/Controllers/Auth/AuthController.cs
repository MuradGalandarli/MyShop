using BusinessLayer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SahredLayer;

namespace ApiLayer.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, 
            ILogger<AuthController> logger,
            IEmailService emailService)
        {
            _authService = authService;
            _logger = logger;
            _emailService = emailService;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message) = await _authService.Login(model);
                if (status == 0)
                    return BadRequest(message);
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("Registeration")]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {

                if (!ModelState.IsValid)
                    return BadRequest("Invalid payload");
                var (status, message) = await _authService.Registeration(model, UserRoles.Admin);
                if (status == 0)
                {
                    return BadRequest(message);
                }
                return CreatedAtAction(nameof(Register), model);            
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string toEmail)
        {
         var data = await _authService.ForgotPassword(toEmail);
            if(data.Item2 == null && data.Item1 == 0)
            {
                return BadRequest("Fail");
            }
            if (data.Item2 != null && data.Item1 == 1)
            {
                await _emailService.SendMail(toEmail,"Forgot email",data.Item2);
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(Resetmodel reset)
        {
            var data = await _authService.ResetPassword(reset);
            if (data.Item2 == null && data.Item1 == 0)
            {
                return BadRequest("Fail");
            }
            if (data.Item2 != null && data.Item1 == 1)
            {             
                return Ok("Successed");
            }
            return BadRequest();
        }


    }
}
