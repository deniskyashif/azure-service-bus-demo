using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;

namespace Sender;

public class SenderWorker : BackgroundService
{
    readonly ILogger<SenderWorker> _logger;
    readonly AzureServiceBusOptions _options;

    public SenderWorker(ILogger<SenderWorker> logger, IOptions<AzureServiceBusOptions> options)
    {
        _logger = logger;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var client = new ServiceBusClient(_options.ConnectionString);
        var sender = client.CreateSender(_options.QueueName);
        var id = 1;
        
        while (!stoppingToken.IsCancellationRequested)
        {
            string xmlMsg = $@"
<CSIPResponse> 
  <SenderApp>CSIP</SenderApp> 
  <SenderAppID>CSIP277136</SenderAppID> 
  <ReceiverApp>LN</ReceiverApp> 
  <SourceApp>SWF</SourceApp> 
  <ReceiverAppMessageID>LN248648</ReceiverAppMessageID> 
  <ResponseType>ACK</ResponseType> 
  <DEALNUMBER>{id}</DEALNUMBER> 
  <DateTime>{DateTime.UtcNow}</DateTime> 
</CSIPResponse>";

            var message = new ServiceBusMessage(xmlMsg);
            await sender.SendMessageAsync(message, stoppingToken);

            _logger.LogInformation($"Sent message #{id++}.");

            Console.WriteLine("Press any key to send a message");
            Console.ReadKey();
		}
    }
}
