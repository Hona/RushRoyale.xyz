using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Hosting;

namespace RushRoyale.Infrastructure;

public class CosmosHostedService : IHostedService
{
    private readonly CosmosClient _cosmosClient;

    public CosmosHostedService(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _cosmosClient.CreateDatabaseIfNotExistsAsync(DbConstants.DatabaseName, cancellationToken: cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}