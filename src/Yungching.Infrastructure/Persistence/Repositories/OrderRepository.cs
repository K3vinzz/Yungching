using System;
using Dapper;
using Yungching.Application.Common.Interfaces;
using Yungching.Domain.Entities;
using Yungching.Domain.Interfaces.Repositories;

namespace Yungching.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    public OrderRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }
    public async Task<int> CreateAsync(Order order)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        connection.Open();

        var sql = @"
                INSERT INTO Orders (CustomerId, EmployeeId, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry)
                VALUES (@CustomerId, @EmployeeId, @OrderDate, @RequiredDate, @ShippedDate, @ShipVia, @Freight, @ShipName, @ShipAddress, @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";

        var newId = await connection.QuerySingleAsync<int>(sql, order);
        return newId;
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

    public async Task<bool> UpdateAsync(Order order)
    {
        using var connection = _dbConnectionFactory.CreateConnection();
        connection.Open();

        var sql = @"
            UPDATE Orders
            SET
                CustomerId = @CustomerId,
                EmployeeId = @EmployeeId,
                OrderDate = @OrderDate,
                RequiredDate = @RequiredDate,
                ShippedDate = @ShippedDate,
                ShipVia = @ShipVia,
                Freight = @Freight,
                ShipName = @ShipName,
                ShipAddress = @ShipAddress,
                ShipCity = @ShipCity,
                ShipRegion = @ShipRegion,
                ShipPostalCode = @ShipPostalCode,
                ShipCountry = @ShipCountry
            WHERE OrderId = @OrderId";

        var affectedRows = await connection.ExecuteAsync(sql, order);
        return affectedRows > 0;
    }
}
