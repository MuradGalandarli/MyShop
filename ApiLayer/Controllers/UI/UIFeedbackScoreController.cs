using AutoMapper;
using BusinessLayer.Service;
using DataAccessLayer.Abstract;
using DataTransferObject.DtoEntity;
using EntityLayer.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLayer.Controllers.UI
{
    // [Authorize(Roles = "admin,user")]
    [Route("api/[controller]")]
    [ApiController]
    public class UIFeedbackScoreController : ControllerBase
    {

        private readonly IFeedbackScoreService _feedbackScore;
        private readonly IMapper _mapper;
        private readonly IValidator<FeedbackScoreDto> _validator;
        public UIFeedbackScoreController(IFeedbackScoreService _feedbackScore,
             IMapper mapper,
             IValidator<FeedbackScoreDto> validator)
        {
            this._feedbackScore = _feedbackScore;
            _mapper = mapper;
            _validator = validator; 
        }
        [HttpPost("GetAllFeedbackScoreUI")]
        public async Task<IActionResult> GetAllFeedbackScore()
        {
            var result = await _feedbackScore.GetAll();
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpGet("GetByIdFeedbackScoreUI/{id}")]
        public async Task<IActionResult> GetByIdFeedbackScore(int id)
        {
            var result = await _feedbackScore.GetById(id);
            return result != null ? Ok(result) : BadRequest();
        }

        [HttpPost("GetProductIdScoreUI/{productId}")]
        public async Task<IActionResult> Score(int productId)
        {
            var result = await _feedbackScore.Score(productId);  
                return (result != null ? Ok(result) : BadRequest());
        }
    }
}
