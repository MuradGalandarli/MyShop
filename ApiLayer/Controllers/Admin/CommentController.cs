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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly IValidator<CommentDto> _validator;
        public CommentController(ICommentService commentService,
            IMapper _mapper,
            IValidator<CommentDto> validator)
        {
            _commentService = commentService;
            this._mapper = _mapper;
            _validator = validator;
        }

        [HttpPost("GetAllComment")]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _commentService.GetAll();
            return (result != null ? Ok(result) : BadRequest());
        }

        [HttpGet("GetByIdComment")]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var result = await _commentService.GetById(id);
            return (result != null ? Ok(result) : BadRequest());
        }

        [HttpPost("AddComment")]
        public async Task<IActionResult> AddCategory(CommentDto t)
        {
            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                var convertComment = _mapper.Map<Comment>(t);
                bool IsSuccess = await _commentService.Add(convertComment);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateCategory(CommentDto t)
        {
            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                var convertComment = _mapper.Map<Comment>(t);
                bool IsSuccess = await _commentService.Update(convertComment);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpDelete("DeleteComment")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool IsSuccess = await _commentService.Delete(id);
            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }


    }
}
