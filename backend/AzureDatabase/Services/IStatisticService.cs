using AzureDatabase.Models;

namespace AzureDatabase.Services;
public interface IStatisticService
{
    public Task<Statistic?> GetStatisticById(string id);
}