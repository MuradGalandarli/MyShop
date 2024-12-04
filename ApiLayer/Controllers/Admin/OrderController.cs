using AutoMapper;
using BusinessLayer.Service;
using DataTransferObject.DtoEntity;
using DataTransferObject.ResponseDto;
using EntityLayer.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiLayer.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService order;
        private readonly IMapper _mapper;
        private readonly IValidator<OrderDto> _validator;
        public OrderController(IOrderService order,
            IMapper mapper,
             IValidator<OrderDto> validator)
        {
            this.order = order;
            _mapper = mapper; 
            _validator = validator;
        }

        [HttpPost("GetAllOrder")]
        public async Task<IActionResult> GetAllOrder()
        {
            var result = await order.GetAll();
            if (result != null)
            {
                var mapOrder = _mapper.Map<List<ResponseOrder>>(result);
                return Ok(mapOrder);

            }
            return BadRequest();
        }

        [HttpGet("GetByIdOrder/{id}")]
        public async Task<IActionResult> GetByIdOrder(int id)
        {
            var result = await order.GetById(id);
            if (result != null)
            {
                var mapOrder = _mapper.Map<ResponseOrder>(result);
                return Ok(mapOrder);

            }
            return BadRequest();
        }
/*
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
*/

        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            bool IsSuccess = await order.Delete(id);
            return (IsSuccess ? Ok(IsSuccess) : BadRequest(IsSuccess));
        }

        [HttpPost("Cancellation/{orderId}")]
        public async Task<IActionResult> Cancellation(int orderId)
        {
           var result = await order.Cancellation(orderId);
            return Ok(result);
        }

        [HttpPost("Selled/{orderId}")]
        public async Task<IActionResult> Selled(int orderId)
        {
            var result = await order.Selled(orderId);
            return Ok(result);
        }

        [HttpGet("GetByIdAddedToCartOrder/{id}")]
        public async Task<IActionResult> GetByIdAddedToCartOrder(int id)
        {
            var result = await order.GetByIdAddedToCartOrder(id);
            if (result != null)
            {
                var mapOrder = _mapper.Map<ResponseOrder>(result);
                return Ok(mapOrder);

            }
            return BadRequest();
        }

        [HttpGet("OrderAddedToCartAllListOrder")]
        public async Task<IActionResult> OrderAddedToCartAllListOrder()
        {
            var result = await order.OrderAddedToCartAllListOrder();
            if (result != null)
            {
                var mapOrder = _mapper.Map<List<ResponseOrder>>(result);
                return Ok(mapOrder);

            }
            return BadRequest();
        }

    }
}
