using System;
using MediatR;
using Yungching.Application.Orders.DTOs;

namespace Yungching.Application.Orders.Queries;

public record GetOrderByIdQuery(int OrderId) : IRequest<OrderDto?>;