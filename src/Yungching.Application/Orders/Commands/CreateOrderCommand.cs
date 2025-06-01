using MediatR;
using Yungching.Application.Orders.DTOs;

namespace Yungching.Application.Orders.Commands;

public record CreateOrderCommand(OrderDto Order) : IRequest<int>;