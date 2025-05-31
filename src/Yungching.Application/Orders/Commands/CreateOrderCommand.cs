using MediatR;
using Yungching.Domain.Entities;

namespace Yungching.Application.Orders.Commands;

public record CreateOrderCommand(Order Order) : IRequest<int>;