using DataTransferObject.DtoEntity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class AboutValidation:AbstractValidator<AboutDto>
    {
        public AboutValidation()
        {
            RuleFor(x => x.Topic).NotEmpty().NotNull();
            RuleFor(x => x.Title).NotEmpty().NotNull();
          
        }
    }
}
