using System;
using AutoMapper;
using Yungching.Application.Orders.DTOs;
using Yungching.Domain.Entities;

namespace Yungching.Application.Common.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderDto, Order>().ReverseMap();
        CreateMap<OrderDetailDto, OrderDetail>().ReverseMap();
        CreateMap<CreateOrderDto, Order>();
        CreateMap<UpdateOrderDto, Order>();
    }
}