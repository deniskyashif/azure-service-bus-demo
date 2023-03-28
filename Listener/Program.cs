using Listener;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builderContext, services) =>
    {
        services.Configure<AzureServiceBusOptions>(
            builderContext.Configuration.GetRequiredSection(nameof(AzureServiceBusOptions)));
        services.AddHostedService<ListenerWorker>();
    })
    .Build();

host.Run();
