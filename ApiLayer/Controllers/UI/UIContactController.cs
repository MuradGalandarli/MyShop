using AutoMapper;
using BusinessLayer.Service;
using DataTransferObject.DtoEntity;
using EntityLayer.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLayer.Controllers.UI
{
    [Route("api/[controller]")]
    [ApiController]
    public class UIContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        private readonly IValidator<ContactDto> _userValidator;
        public UIContactController(IContactService contactService,
            IMapper _mapper,
            IValidator<ContactDto> _userValidator)
        {
            _contactService = contactService;
            this._mapper = _mapper;
            this._userValidator = _userValidator;   
        }

        [HttpPost("AddContact")]
        public async Task<IActionResult> AddContact([FromBody]ContactDto t)
        {
            var validationResult = await _userValidator.ValidateAsync(t);
            if (validationResult.IsValid)
            {
                var convertContact = _mapper.Map<Contact>(t);
                var IsSuccess = await _contactService.Add(convertContact);
                return IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess);
            }
           
            return BadRequest("False");
        }


    }
}
