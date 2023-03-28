namespace Listener;

public class ListenerWorker : BackgroundService
{
    readonly ILogger<ListenerWorker> _logger;
    readonly AzureServiceBusOptions _options;
    
    public ListenerWorker(ILogger<ListenerWorker> logger, AzureServiceBusOptions options)
    {
        _logger = logger;
        _options = options;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {

        }
    }
}
