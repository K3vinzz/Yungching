using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yungching.Application.Orders.Commands;
using Yungching.Application.Orders.DTOs;
using Yungching.Application.Orders.Queries;
using Yungching.Domain.Interfaces.Repositories;

namespace Yungching.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediator _mediator;

        public OrdersController(IOrderRepository orderRepository, IMediator mediator)
        {
            _orderRepository = orderRepository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var query = new GetOrdersQuery();
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var query = new GetOrderByIdQuery(id);
            var order = await _mediator.Send(query);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateOrderDto createOrderDto)
        {
            var command = new CreateOrderCommand(createOrderDto);

            var createdOrderId = await _mediator.Send(command);
            if (createdOrderId <= 0)
                return BadRequest("Failed to create order");

            return CreatedAtAction(nameof(GetById), new { id = createdOrderId }, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(UpdateOrderDto updateOrderDto)
        {
            var command = new UpdateOrderCommand(updateOrderDto);

            var success = await _mediator.Send(command);
            if (!success)
                return NotFound();

            return NoContent();
        }



    }
}
