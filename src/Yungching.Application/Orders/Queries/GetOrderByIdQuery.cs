using System;
using MediatR;
using Yungching.Domain.Entities;

namespace Yungching.Application.Orders.Queries;

public record GetOrderByIdQuery(int OrderId) : IRequest<Order?>;