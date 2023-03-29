using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using Sender;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService(options => options.ServiceName = ".NET ASB Sender Service");

LoggerProviderOptions.RegisterProviderOptions<EventLogSettings, EventLogLoggerProvider>(builder.Services);

builder.Services.Configure<AzureServiceBusOptions>(builder.Configuration.GetRequiredSection(nameof(AzureServiceBusOptions)));
builder.Services.AddHostedService<SenderWorker>();

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

IHost host = builder.Build();
host.Run();