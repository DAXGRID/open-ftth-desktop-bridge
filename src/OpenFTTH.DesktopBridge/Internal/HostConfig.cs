using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Compact;
using OpenFTTH.DesktopBridge.Bridge;
using OpenFTTH.DesktopBridge.GeographicalAreaUpdated;
using OpenFTTH.DesktopBridge.Config;
using System.Net;
using MediatR;

namespace OpenFTTH.DesktopBridge.Internal
{
    public static class HostConfig
    {
        public static IHost Configure()
        {
            var hostBuilder = new HostBuilder();
            ConfigureApp(hostBuilder);
            ConfigureServices(hostBuilder);
            ConfigureLogging(hostBuilder);

            return hostBuilder.Build();
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
                services.AddTransient<IBridgeSessionFactory, BridgeSessionFactory>();
                services.AddTransient<IGeographicalAreaUpdatedConsumer, GeographicalAreaUpdatedKafkaConsumer>();

                services.Configure<KafkaSetting>(kafkaSettings =>
                                                 hostContext.Configuration.GetSection("kafka").Bind(kafkaSettings));

                services.AddSingleton<BridgeServer>(
                    x => new BridgeServer(
                        IPAddress.Any,
                        5000,
                        x.GetRequiredService<IBridgeSessionFactory>(),
                        x.GetRequiredService<Microsoft.Extensions.Logging.ILogger<BridgeServer>>()));


                services.Configure<KafkaSetting>(kafkaSettings =>
                                                 hostContext.Configuration.GetSection("kafka").Bind(kafkaSettings));
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
}
