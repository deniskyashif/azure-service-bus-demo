using Sender;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builderContext, services) =>
    {
        IConfiguration configuration = builderContext.Configuration;
		var settings = configuration.GetRequiredSection("AzureServiceBusOptions").Get<AzureServiceBusOptions>();
		services.Configure<AzureServiceBusOptions>(configuration.GetRequiredSection("AzureServiceBusOptions"));

		services.AddHostedService<SenderWorker>();
    })
    .Build();

host.Run();
