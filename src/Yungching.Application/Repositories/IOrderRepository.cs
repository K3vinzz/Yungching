using System;
using Yungching.Domain.Entities;

namespace Yungching.Application.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(int orderId);
    Task<IEnumerable<Order>> GetAllAsync();
    Task<int> CreateAsync(Order order);
    Task<bool> UpdateAsync(Order order);
    Task<bool> DeleteAsync(int orderId);
}
