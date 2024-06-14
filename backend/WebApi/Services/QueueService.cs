using Common.Services;
using Microsoft.ApplicationInsights;
using Azure.Storage.Queues;
using System.Text;

namespace Queue.Services;
public class QueueService : IQueueService
{
    private readonly ILogger<QueueService> _logger;
    private readonly TelemetryClient _telemetryClient;
    private readonly IConfigurationService _configurationService;
    private readonly QueueClient _queueClient;

    public QueueService(ILogger<QueueService> logger, TelemetryClient telemetryClient, IConfigurationService configurationService)
    {
        _logger = logger;
        _configurationService = configurationService;
        _telemetryClient = telemetryClient;

        var connectionString = _configurationService.GetSecretQueueConnectionString();
        var queueName = _configurationService.GetStorageQueueName();
        _queueClient = new QueueClient(connectionString, queueName);


    }
    public async Task<string?> GetMessage(bool peek = false)
    {

        if (peek)
        {
            var message = await _queueClient.PeekMessageAsync();
            if (message.Value == null)
            {
                return null;
            }
            return DecodeBase64(message.Value.Body.ToString());

        }
        else
        {
            var message = await _queueClient.ReceiveMessageAsync();
            if (message.Value == null)
            {
                return null;
            }
            var id = message.Value.MessageId;
            var pop = message.Value.PopReceipt;
            await _queueClient.DeleteMessageAsync(id, pop);
            return DecodeBase64(message.Value.Body.ToString());

        }
    }

    public async Task SendMessage(string message)
    {
        await _queueClient.SendMessageAsync(EncodeBase64(message));
    }

    public async Task<int> GetQueueLength()
    {
        var properties = await _queueClient.GetPropertiesAsync();
        return properties.Value.ApproximateMessagesCount;
    }

    public static string DecodeBase64(string base64EncodedData)
    {
        byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        string decodedString = Encoding.UTF8.GetString(base64EncodedBytes);

        return decodedString;
    }

    public static string EncodeBase64(string plainText)
    {
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        string base64EncodedData = Convert.ToBase64String(plainTextBytes);

        return base64EncodedData;
    }
}


