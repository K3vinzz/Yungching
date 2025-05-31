using System;
using MediatR;
using Yungching.Domain.Entities;

namespace Yungching.Application.Orders.Queries;

public record GetOrdersQuery() : IRequest<IEnumerable<Order>>;
