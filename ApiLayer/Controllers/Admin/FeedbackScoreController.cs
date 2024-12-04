using AutoMapper;
using BusinessLayer.Helpers;
using BusinessLayer.Service;
using DataTransferObject.DtoEntity;
using EntityLayer.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLayer.Controllers.Admin
{
    [Authorize(Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackScoreController : ControllerBase
    {
        private readonly IFeedbackScoreService _feedbackScore;
        private readonly IMapper _mapper;
        private readonly IValidator<FeedbackScoreDto> _validator;
        public FeedbackScoreController(IFeedbackScoreService _feedbackScore,
             IMapper mapper,
             IValidator<FeedbackScoreDto> validator)
        {
            this._feedbackScore = _feedbackScore;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost("AddFeedbackScoreUI")]
        public async Task<IActionResult> AddCategory(FeedbackScoreDto t)
        {
            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                string token = Request.Headers["Authentication"];
                string userId = TokenHelper.ProcessToken(token);
                var convertFeedbackScore = _mapper.Map<FeedbackScore>(t);
                convertFeedbackScore.UserIdFromToken = userId;
                bool IsSuccess = await _feedbackScore.Add(convertFeedbackScore);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpPut("UpdateFeedbackScoreUI")]
        public async Task<IActionResult> UpdateFeedbackScore(FeedbackScoreDto t)
        {
            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                string token = Request.Headers["Authentication"];
                string userId = TokenHelper.ProcessToken(token);
                var convertFeedbackScore = _mapper.Map<FeedbackScore>(t);
                convertFeedbackScore.UserIdFromToken = userId;
                bool IsSuccess = await _feedbackScore.Update(convertFeedbackScore);
                return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
            }
            return BadRequest();
        }

        [HttpDelete("DeleteFeedbackScoreUI/{id}")]
        public async Task<IActionResult> DeleteFeedbackScore(int id)
        {
            bool IsSuccess = await _feedbackScore.Delete(id);
            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }



    }
}
