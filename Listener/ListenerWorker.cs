using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;

namespace Listener;

public class ListenerWorker : BackgroundService
{
    readonly ILogger<ListenerWorker> _logger;
    readonly AzureServiceBusOptions _options;
    
    public ListenerWorker(ILogger<ListenerWorker> logger, IOptions<AzureServiceBusOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var client = new ServiceBusClient(_options.ConnectionString);
        
        var receiver = client.CreateReceiver(_options.QueueName, new ServiceBusReceiverOptions
        {
			// The receiver has a lock on the message for a particular duration.
            // If the message is not settled by this time, it lands back on Service Bus
            // to be fetched by the next receive operation
			ReceiveMode = ServiceBusReceiveMode.PeekLock
		});
        
        while (!stoppingToken.IsCancellationRequested)
        {
            var receivedMessage = await receiver.ReceiveMessageAsync();
            _logger.LogInformation("Received message: {0}", receivedMessage.Body);
            
            await receiver.CompleteMessageAsync(receivedMessage); // Deletes the message from the queue
        }
    }
}
