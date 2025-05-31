using MediatR;
using Yungching.Domain.Entities;
using Yungching.Application.Orders.Commands;
using Yungching.Application.Orders.Queries;
using Yungching.Domain.Interfaces.Repositories;

namespace Yungching.Application.Orders.Handlers
{
    public class OrderHandlers :
        IRequestHandler<GetOrdersQuery, IEnumerable<Order>>,
        IRequestHandler<GetOrderByIdQuery, Order?>,
        IRequestHandler<CreateOrderCommand, int>,
        IRequestHandler<UpdateOrderCommand, bool>,
        IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderHandlers(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetByIdAsync(request.OrderId);
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepository.AddAsync(request.Order);
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepository.UpdateAsync(request.Order);
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepository.DeleteAsync(request.OrderId);
        }
    }
}
