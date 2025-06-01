using MediatR;
using Yungching.Application.Orders.DTOs;

namespace Yungching.Application.Orders.Commands;

public record UpdateOrderCommand(UpdateOrderDto Order) : IRequest<bool>;