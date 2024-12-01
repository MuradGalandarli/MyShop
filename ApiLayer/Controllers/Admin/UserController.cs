using AutoMapper;
using BusinessLayer.Service;
using DataTransferObject.DtoEntity;
using EntityLayer.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLayer.Controllers.Admin
{
    [Authorize (Roles ="admin,user")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IValidator<UserDto> _validator;
        public UserController(IUserService _userService,
            IMapper mapper,
            IValidator<UserDto> _validator)
        {
           this._userService = _userService;
           _mapper = mapper;
            this._validator = _validator;
        }

        [HttpPost("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _userService.GetAll();
            return (result != null ? Ok(result) : BadRequest());
        }

        [HttpGet("GetByIdUser{id}")]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            var result = await _userService.GetById(id);
            return (result != null ? Ok(result) : BadRequest());
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserDto t)
        {
            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                var convertUser = _mapper.Map<User>(t);
                bool IsSuccess = await _userService.Add(convertUser);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserDto t)
        {

            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                var convertUser = _mapper.Map<User>(t);
                bool IsSuccess = await _userService.Update(convertUser);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool IsSuccess = await _userService.Delete(id);
            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }

    }
}
