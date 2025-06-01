using MediatR;
using Yungching.Domain.Entities;
using Yungching.Application.Orders.Commands;
using Yungching.Application.Orders.Queries;
using Yungching.Domain.Interfaces.Repositories;
using AutoMapper;
using Yungching.Application.Orders.DTOs;

namespace Yungching.Application.Orders.Handlers
{
    public class OrderHandlers :
        IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>,
        IRequestHandler<GetOrderByIdQuery, OrderDto?>,
        IRequestHandler<CreateOrderCommand, int>,
        IRequestHandler<UpdateOrderCommand, bool>,
        IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderHandlers(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            return _mapper.Map<OrderDto?>(order);
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request.Order);
            return await _orderRepository.CreateAsync(order);
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request.Order);
            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepository.DeleteAsync(request.OrderId);
        }
    }
}
