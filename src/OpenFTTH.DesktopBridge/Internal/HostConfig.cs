using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using OpenFTTH.DesktopBridge.Bridge;
using OpenFTTH.DesktopBridge.Config;
using OpenFTTH.DesktopBridge.Event;
using OpenFTTH.DesktopBridge.GeographicalAreaUpdated;
using OpenFTTH.DesktopBridge.Notification;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System.Net;

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
            services.AddTransient<INotificationConsumer, NotificationConsumer>();
            services.AddTransient<IEventMapper, EventMapper>();

            services.Configure<NotificationServerSetting>(
                notificationServerSettings =>
                hostContext.Configuration.GetSection("notificationServer").Bind(notificationServerSettings));

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
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                    .MinimumLevel.Information()
                    .ReadFrom.Configuration(loggingConfiguration)
                    .Enrich.FromLogContext()
                    .WriteTo.Console(new CompactJsonFormatter())
                    .CreateLogger();

                loggingBuilder.AddSerilog(logger, true);
            });
        });
    }
}
