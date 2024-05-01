using Microsoft.Azure.Cosmos;
using System.Data.Common;
using Common.Services;
using AzureDatabase.Models;

namespace AzureDatabase.Services;
public class StatisticService : IStatisticService
{
    private readonly CosmosClient _cosmosClient;
    private readonly IConfigurationService _configurationService;


    public StatisticService(IConfigurationService configurationService)
    {
        _configurationService = configurationService;

        var connectionString = _configurationService.GetDatabaseConnectionString();
        DbConnectionStringBuilder builder = new() { ConnectionString = connectionString };

        string endpointUri = builder["AccountEndpoint"]?.ToString() ?? "";
        string primaryKey = builder["AccountKey"]?.ToString() ?? "";

        _cosmosClient = new CosmosClient(endpointUri, primaryKey);
    }

    public async Task<Statistic?> GetStatisticById(string id)
    {
        var database = _cosmosClient.GetDatabase("Learn_Azure_DB");
        var container = database.GetContainer("Statistics");

        try
        {
            var response = await container.ReadItemAsync<Statistic>(id, new PartitionKey(id));
            return response.Resource;

        }
        catch (CosmosException)
        {
            return null;
        }
    }
}