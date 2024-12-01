using DataTransferObject.DtoEntity;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
{
    public class OrderValidator:AbstractValidator<OrderDto>
    {
        public OrderValidator()
        {
            RuleFor(x => x.OrderCount).NotEmpty().NotNull();
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.TotalAmount).NotEmpty().NotNull();
            RuleFor(x => x.ProductId).NotEmpty().NotNull();
        }
    }
}
