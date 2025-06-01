using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yungching.Application.Orders.DTOs;
using Yungching.Application.Repositories;

namespace Yungching.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var orders = await _orderRepository.GetAllAsync();
            return Ok(orders);
        }
    }
}
