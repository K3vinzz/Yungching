using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Yungching.Application.Common.Interfaces;

namespace Yungching.Infrastructure.Persistence;

public class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
