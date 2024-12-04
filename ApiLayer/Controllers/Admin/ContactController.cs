using AutoMapper;
using BusinessLayer.Service;
using DataTransferObject.DtoEntity;
using DataTransferObject.ResponseDto;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLayer.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
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
            if (result != null)
            {
                var mapContact = _mapper.Map<List<ResponseContact>>(result);
                return Ok(mapContact);

            }
            return BadRequest();
        }

        [HttpGet("GetByIdContact/{id}")]
        public async Task<IActionResult> GetByIdContact(int id)
        {
            var result = await _contactService.GetById(id);
            if (result != null)
            {
                var mapContact = _mapper.Map<ResponseContact>(result);
                return Ok(mapContact);

            }
            return BadRequest();
        }

        [HttpPut("UpdateContact")]
        public async Task<IActionResult> UpdateContact(ContactDto t)
        {
            var convertContact = _mapper.Map<Contact>(t);
            bool IsSuccess = await _contactService.Update(convertContact);
            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }

        [HttpDelete("DeleteContact/{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            bool IsSuccess = await _contactService.Delete(id);
            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }



    }
}
