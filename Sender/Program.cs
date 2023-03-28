using Sender;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builderContext, services) =>
    {
		services.Configure<AzureServiceBusOptions>(
            builderContext.Configuration.GetRequiredSection(nameof(AzureServiceBusOptions)));

		services.AddHostedService<SenderWorker>();
    })
    .Build();

host.Run();
