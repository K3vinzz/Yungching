using System;

namespace Yungching.Application.Orders.DTOs;

public class OrderDetailDto
{
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public short Quantity { get; set; }
    public float Discount { get; set; }
}
