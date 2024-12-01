using DataTransferObject.DtoEntity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class CategoryValidator:AbstractValidator<CategoryDto>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().NotNull();
        }
    }
}
