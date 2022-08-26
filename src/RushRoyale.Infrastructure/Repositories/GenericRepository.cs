using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.Azure.Cosmos;

namespace RushRoyale.Infrastructure.Repositories;

public class GenericRepository
{
    private readonly CosmosClient _cosmosClient;

    public GenericRepository(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
    }

    private async Task<Container> GetContainerAsync<T>(Expression<Func<T, string>> partitionKeyPath) where T : ICosmosDocument
    {
        var result = await _cosmosClient.CreateDatabaseIfNotExistsAsync(DbConstants.DatabaseName);
        
        var database = result.Database;
        var response = await database.CreateContainerIfNotExistsAsync(typeof(T).FullName, "/" + ((partitionKeyPath.Body as MemberExpression)?.Member.Name ?? throw new InvalidCastException("Cannot cast partition key expression to MemberExpression")));
        return response.Container;
    }
    

    public async Task<T?> GetItemAsync<T>(T model, Func<T, string> idExpression, Expression<Func<T, string>> partitionKeyExpression) where T : ICosmosDocument
    {
        var container = await GetContainerAsync(partitionKeyExpression);

        try
        {
            var getPartitionKey = partitionKeyExpression.Compile();
            var response = await container.ReadItemAsync<T>(idExpression(model), new PartitionKey(getPartitionKey(model)));
            return response.Resource;
        }
        catch (CosmosException e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            return default;
        }
    }
    
    public async Task<IEnumerable<T>> GetItemListAsync<T>(string partitionKey, Expression<Func<T, string>> partitionKeyExpression) where T : ICosmosDocument
    {
        var container = await GetContainerAsync(partitionKeyExpression);

        try
        {
            var output = new List<T>();

            using var resultSet = container.GetItemQueryIterator<T>(
                queryDefinition: null,
                requestOptions: new QueryRequestOptions
                {
                    PartitionKey = new PartitionKey(partitionKey)
                });
            
            while (resultSet.HasMoreResults)
            {
                var response = await resultSet.ReadNextAsync();
                    
                output.AddRange(response);
            }

            return output;
        }
        catch (CosmosException e) when (e.StatusCode == HttpStatusCode.NotFound)
        {
            return Enumerable.Empty<T>();
        }
    }

    
    public async Task CreateItemAsync<T>(T item, Expression<Func<T, string>> partitionKeyExpression) where T : ICosmosDocument
    {
        var container = await GetContainerAsync(partitionKeyExpression);
        var getPartitionKey = partitionKeyExpression.Compile();

        await container.CreateItemAsync(item, new PartitionKey(getPartitionKey(item)));
    }
    
    public async Task CreateOrUpdateItemAsync<T>(T item, Expression<Func<T, string>> partitionKeyExpression) where T : ICosmosDocument
    {
        var container = await GetContainerAsync(partitionKeyExpression);
        var getPartitionKey = partitionKeyExpression.Compile();

        await container.UpsertItemAsync(item, new PartitionKey(getPartitionKey(item)));
    }
    
    public async Task DeleteItemAsync<T>(T item, Expression<Func<T, string>> partitionKeyExpression) where T : ICosmosDocument
    {
        var container = await GetContainerAsync(partitionKeyExpression);
        
        var getPartitionKey = partitionKeyExpression.Compile();
        await container.DeleteItemAsync<T>(item.Id, new PartitionKey(getPartitionKey(item)));
    }
}