using MediatR;
using Yungching.Application.Orders.DTOs;

namespace Yungching.Application.Orders.Commands;

public record CreateOrderCommand(CreateOrderDto Order) : IRequest<int>;