using DataTransferObject.DtoEntity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validation
    {
        public class ContactValidation:AbstractValidator<ContactDto>
        {
            public ContactValidation()
            {
                RuleFor(contact => contact.Name).NotEmpty().NotNull();
                RuleFor(contact => contact.Email).NotEmpty().NotNull().EmailAddress();
                RuleFor(contact => contact.Message).NotEmpty().NotNull();
                RuleFor(contact => contact.Subject).NotEmpty().NotNull();
            }
        }
    }
