using MediatR;
using Yungching.Domain.Entities;

namespace Yungching.Application.Orders.Commands;

public record UpdateOrderCommand(Order Order) : IRequest<bool>;