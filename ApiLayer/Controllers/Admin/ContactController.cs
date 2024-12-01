using AutoMapper;
using BusinessLayer.Service;
using DataTransferObject.DtoEntity;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLayer.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        public ContactController(IContactService _contactService,
             IMapper _mapper)
        {
            this._contactService = _contactService;
            this._mapper = _mapper;
        }


        [HttpPost("GetAllContact")]
        public async Task<IActionResult> GetAllContact()
        {
            var result = await _contactService.GetAll();
            return (result != null ? Ok(result) : BadRequest());
        }

        [HttpGet("GetByIdContact{id}")]
        public async Task<IActionResult> GetByIdContact(int id)
        {
            var result = await _contactService.GetById(id);
            return (result != null ? Ok(result) : BadRequest());
        }

        [HttpPut("UpdateContact")]
        public async Task<IActionResult> UpdateContact(ContactDto t)
        {
            var convertContact = _mapper.Map<Contact>(t);
            bool IsSuccess = await _contactService.Update(convertContact);
            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }

        [HttpDelete("DeleteContact{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            bool IsSuccess = await _contactService.Delete(id);
            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }



    }
}
