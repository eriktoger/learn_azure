namespace Queue.Services;
public interface IQueueService
{
    public Task<string?> GetMessage(bool peek);
    public Task<int> GetQueueLength();
    public Task SendMessage(string message);
}