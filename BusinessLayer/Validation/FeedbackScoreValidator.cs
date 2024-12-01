using DataTransferObject.DtoEntity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class FeedbackScoreValidator:AbstractValidator<FeedbackScoreDto>
    {
        public FeedbackScoreValidator()
        {
            RuleFor(x => x.CountStar).NotEmpty().NotNull();
            RuleFor(x => x.ProductId).NotEmpty().NotNull();
            RuleFor(x => x.UserId).NotEmpty().NotNull();
        }
    }
}
