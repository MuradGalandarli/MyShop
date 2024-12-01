using DataTransferObject.DtoEntity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class CommentValidator:AbstractValidator<CommentDto>
    {
        public CommentValidator() 
        {
            RuleFor(x => x.Feedback).NotEmpty().NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty().NotEmpty();
            RuleFor(x => x.UserId).NotEmpty().NotEmpty();
          
        }
    }
}
