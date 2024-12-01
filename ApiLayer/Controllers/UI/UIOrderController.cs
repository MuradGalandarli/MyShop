using BusinessLayer.Service;
using EntityLayer.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using AutoMapper;
using DataTransferObject.DtoEntity;
using FluentValidation;

namespace ApiLayer.Controllers.UI
{
    // [Authorize(Roles = "admin,user")]
    [Route("api/[controller]")]
    [ApiController]
    public class UIOrderController : ControllerBase
    {
        private readonly IOrderService order;
        private readonly IMapper _mapper;
        private readonly IValidator<OrderDto> _validator;
        public UIOrderController(IOrderService order,
            IMapper _mapper,
            IValidator<OrderDto> validator)
        {
            this.order = order;
            this._mapper = _mapper;
            _validator = validator;
        }

        [HttpPost("GetAllOrderUI")]
        public async Task<IActionResult> GetAllOrder()
        {
            var result = await order.GetAll();
            return (result != null ? Ok(result) : BadRequest());
        }

        [HttpGet("GetByIdOrderUI")]
        public async Task<IActionResult> GetByIdOrder(int id)
        {
            var result = await order.GetById(id);
            return (result != null ? Ok(result) : BadRequest());
        }

        [HttpGet("BetsSeller")]
        public async Task<IActionResult> BestSeller()
        {
            var result = await order.BestSeller();
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(result, options);

            return (json != null ? Ok(json) : BadRequest());

        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder(OrderDto t)
        {
            var resultValid = await _validator.ValidateAsync(t);
            if (resultValid.IsValid)
            {
                var convertOrder = _mapper.Map<Order>(t);
                var result = await order.AddOrder(convertOrder);
                return (result != null ? Ok(result) : BadRequest());
            }
            return BadRequest();
        }


    }
}
