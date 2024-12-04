using AutoMapper;
using BusinessLayer.Helpers;
using BusinessLayer.Service;
using DataTransferObject.DtoEntity;
using DataTransferObject.ResponseDto;
using EntityLayer.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLayer.Controllers.Admin
{
    [Authorize (Roles ="Admin,User")]
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
            if (result != null)
            {
                var mapUser = _mapper.Map<ResponseAbout>(result);
                return Ok(mapUser);

            }
            return BadRequest();
        }

        [HttpGet("GetByIdUser{id}")]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            var result = await _userService.GetById(id);
            if (result != null)
            {
                var mapUser = _mapper.Map<ResponseAbout>(result);
                return Ok(mapUser);

            }
            return BadRequest();
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(UserDto t)
        {
            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                string token = Request.Headers["Authorization"];
                string userId = TokenHelper.ProcessToken(token);
                var convertUser = _mapper.Map<User>(t);
                convertUser.ApplicationUserId = userId;
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
                string token = Request.Headers["Authorization"];
                string userId = TokenHelper.ProcessToken(token);
                var convertUser = _mapper.Map<User>(t);
                convertUser.ApplicationUserId = userId;
                bool IsSuccess = await _userService.Update(convertUser);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
           
            bool IsSuccess = await _userService.Delete(id);
            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }

        [HttpDelete("DeleteUserWithToken")]
        public async Task<IActionResult> DeleteUserWithToken()
        {
            string token = Request.Headers["Authorization"];
            string userId = TokenHelper.ProcessToken(token);
            bool IsSuccess = await _userService.DeleteUserWithToken(userId);
            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }

        [HttpGet("GetByIdUser")]
        public async Task<IActionResult> GetByIdUser()
        {
            string token = Request.Headers["Authorization"];
            string userId = TokenHelper.ProcessToken(token);
            var result = await _userService.GetByIdWithToken(userId);
            var mapUser = _mapper.Map<User>(result);    
            return (mapUser != null ? Ok(mapUser) : BadRequest());
        }
    }
}
