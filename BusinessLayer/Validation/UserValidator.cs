using DataTransferObject.DtoEntity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class UserValidator:AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull();
            RuleFor(x => x.SureName).NotEmpty().NotNull();
        }
    }
}
