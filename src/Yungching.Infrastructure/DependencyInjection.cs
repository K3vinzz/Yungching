using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yungching.Application.Common.Interfaces;
using Yungching.Domain.Interfaces.Repositories;
using Yungching.Infrastructure.Persistence;
using Yungching.Infrastructure.Persistence.Repositories;

namespace Yungching.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDbConnectionFactory>(sp =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            return new SqlConnectionFactory(connectionString!);
        });

        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
