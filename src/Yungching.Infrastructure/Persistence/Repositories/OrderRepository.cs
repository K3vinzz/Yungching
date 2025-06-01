using System;
using Dapper;
using Yungching.Application.Common.Interfaces;
using Yungching.Application.Repositories;
using Yungching.Domain.Entities;

namespace Yungching.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    public OrderRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    public Task<int> CreateAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        connection.Open();

        var sql = @"SELECT * FROM Orders";
        var orders = await connection.QueryAsync<Order>(sql);

        return orders;
    }

    public async Task<Order?> GetByIdAsync(int orderId)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        connection.Open();

        var sql = @"SELECT * FROM Orders WHERE OrderId = @OrderId";
        var order = await connection.QueryFirstOrDefaultAsync<Order>(sql, new { OrderId = orderId });

        return order;
    }

    public Task<bool> UpdateAsync(Order order)
    {
        throw new NotImplementedException();
    }
}
