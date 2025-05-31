using MediatR;

namespace Yungching.Application.Orders.Commands;

public record DeleteOrderCommand(int OrderId) : IRequest<bool>;
