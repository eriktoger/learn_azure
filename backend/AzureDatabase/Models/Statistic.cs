using System.Text.Json.Serialization;

namespace AzureDatabase.Models;

public record Statistic()
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("counter")]
    public int Counter { get; set; }

}
