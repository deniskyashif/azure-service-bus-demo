namespace Listener;

public class AzureServiceBusOptions
{
    public string ConnectionString { get; set; }
    public string TopicName { get; set; }
    public string SubscriptionName { get; set; }
}