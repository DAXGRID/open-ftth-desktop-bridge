using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Compact;
using OpenFTTH.DesktopBridge.Bridge;
using OpenFTTH.DesktopBridge.GeographicalAreaUpdated;
using OpenFTTH.DesktopBridge.Config;
using OpenFTTH.DesktopBridge.Event;
using System.Net;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace OpenFTTH.DesktopBridge.Internal;

public static class HostConfig
{
    public static IHost Configure()
    {
        var hostBuilder = new HostBuilder();
        ConfigureApp(hostBuilder);
        ConfigureServices(hostBuilder);
        ConfigureLogging(hostBuilder);
        ConfigureJsonConverter();

        return hostBuilder.Build();
    }

    private static void ConfigureJsonConverter()
    {
        JsonConvert.DefaultSettings = (() =>
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Converters.Add(new StringEnumConverter());
            settings.TypeNameHandling = TypeNameHandling.Auto;

            return settings;
        });
    }

    private static void ConfigureApp(IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddEnvironmentVariables();
        });
    }

    private static void ConfigureServices(IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((hostContext, services) =>
        {
            services.AddOptions();
            services.AddMediatR(typeof(Program));

            services.AddHostedService<DesktopBridgeHost>();
            services.AddTransient<IGeographicalAreaUpdatedConsumer, GeographicalAreaUpdatedKafkaConsumer>();
            services.AddTransient<IEventMapper, EventMapper>();

            services.Configure<KafkaSetting>(kafkaSettings =>
                                             hostContext.Configuration.GetSection("kafka").Bind(kafkaSettings));

            services.AddSingleton<IBridgeServer, BridgeServer>(
                x => new BridgeServer(
                    IPAddress.Any,
                    5000,
                    x.GetRequiredService<Microsoft.Extensions.Logging.ILogger<BridgeServer>>(),
                    x.GetRequiredService<IMediator>(),
                    x.GetRequiredService<IEventMapper>()
                    )
                );
        });
    }

    private static void ConfigureLogging(IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices((hostContext, services) =>
        {
            var loggingConfiguration = new ConfigurationBuilder()
               .AddEnvironmentVariables().Build();

            services.AddLogging(loggingBuilder =>
            {
                var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(loggingConfiguration)
                    .Enrich.FromLogContext()
                    .WriteTo.Console(new CompactJsonFormatter())
                    .CreateLogger();

                loggingBuilder.AddSerilog(logger, true);
            });
        });
    }
}
