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
        public async Task<IActionResult> GetAllCommet()
        {
            var result = await _commentService.GetAll();
            if (result != null)
            {
                var mapComment = _mapper.Map<List<ResponseComment>>(result);
                return Ok(mapComment);

            }
            return BadRequest();
        }

        [HttpGet("GetByIdComment")]
        public async Task<IActionResult> GetByIdComment(int id)
        {
            var result = await _commentService.GetById(id);
            if (result != null)
            {
                var mapComment = _mapper.Map<ResponseComment>(result);
                return Ok(mapComment);

            }
            return BadRequest();
        }

        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment(CommentDto t)
        {
            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                string token = Request.Headers["Authentication"];
                string userId = TokenHelper.ProcessToken(token);
                var convertComment = _mapper.Map<Comment>(t);
                convertComment.UserIdFromToken = userId;
                bool IsSuccess = await _commentService.Add(convertComment);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateComment(CommentDto t)
        {
            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                string token = Request.Headers["Authentication"];
                string userId = TokenHelper.ProcessToken(token);
                var convertComment = _mapper.Map<Comment>(t);
                convertComment.UserIdFromToken = userId;
                bool IsSuccess = await _commentService.Update(convertComment);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpDelete("DeleteComment")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            bool IsSuccess = await _commentService.Delete(id);
            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }

        [HttpGet("GetByProductIdAllComment/{productId}")]
        public async Task<IActionResult> GetByProductIdAllComment(int productId)
        {
           var result = await _commentService.GetByProductIdAllComment(productId);
            if (result != null)
            {
                var mapAbout = _mapper.Map<List<ResponseComment>>(result);
                return Ok(mapAbout);
            }
            return BadRequest();
             
        }


    }
}
