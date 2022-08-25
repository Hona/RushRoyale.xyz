using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using RushRoyale.Infrastructure.Repositories;

namespace RushRoyale.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string cosmosConnectionString)
    {
        services.AddSingleton(new CosmosClient(cosmosConnectionString));
        services.AddSingleton<GenericRepository>();
        
        services.AddHostedService<CosmosHostedService>();

        return services;
    }
}