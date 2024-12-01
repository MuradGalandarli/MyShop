using DataTransferObject.DtoEntity;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class FavoriteProductValidator:AbstractValidator<FavoriteProductDto>
    {
        public FavoriteProductValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.ProductId).NotEmpty().NotNull();
        }
    }
}
