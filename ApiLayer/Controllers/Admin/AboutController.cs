using AutoMapper;
using BusinessLayer.Service;
using DataTransferObject.DtoEntity;
using EntityLayer.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace ApiLayer.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;
        private readonly IMapper _mapper;
        private readonly IValidator<AboutDto> _validator;
     
        public AboutController(IAboutService aboutService,
            IMapper _mapper,
            IValidator<AboutDto> validator
           )
        {
             _aboutService = aboutService;
            this._mapper = _mapper;
            _validator = validator;
          
        }

        [HttpPost("GetAllAbout")]
        public async Task<IActionResult> GetAllAbout()
        {
            var result = await _aboutService.GetAll();
            return (result != null ? Ok(result) : BadRequest());
        }

        [HttpGet("GetByIdAbout{id}")]
        public async Task<IActionResult> GetByIdAbout(int id)
        {
            var result = await _aboutService.GetById(id);
            return (result != null ? Ok(result) : BadRequest());
        }

        [HttpPost("AddAbout")]
        public async Task<IActionResult> AddAbout([FromForm] AboutDto t)
        {
            var validationResult = await _validator.ValidateAsync(t);
            if (validationResult.IsValid)
            {
                var convertAbout = _mapper.Map<About>(t);
                bool IsSuccess = await _aboutService.Add(convertAbout);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpPut("UpdateAbout")]
        public async Task<IActionResult> UpdateAbout([FromForm] AboutDto t)
        {
            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                var convertAbout = _mapper.Map<About>(t);
                bool IsSuccess = await _aboutService.Update(convertAbout);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();

        }

        [HttpDelete("DeleteAbout/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool IsSuccess = await _aboutService.Delete(id);
            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }

        [HttpGet("IsActiveAbout{id}")]
        public async Task<IActionResult> IsActiveAbout(int id)
        {
            await _aboutService.IsActive(id);
            return Ok();
        }

    }
}
