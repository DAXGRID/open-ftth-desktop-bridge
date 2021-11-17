using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using OpenFTTH.DesktopBridge.GeographicalAreaUpdated;

namespace OpenFTTH.DesktopBridge.Bridge;

public class DesktopBridgeHost : IHostedService
{
    private readonly ILogger<DesktopBridgeHost> _logger;
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IBridgeServer _bridgeServer;
    private readonly IGeographicalAreaUpdatedConsumer _geographicalAreaUpdatedConsumer;

    public DesktopBridgeHost(
        ILogger<DesktopBridgeHost> logger,
        IHostApplicationLifetime applicationLifetime,
        IBridgeServer bridgeServer,
        IGeographicalAreaUpdatedConsumer geographicalAreaUpdatedConsumer)
    {
        _logger = logger;
        _applicationLifetime = applicationLifetime;
        _bridgeServer = bridgeServer;
        _geographicalAreaUpdatedConsumer = geographicalAreaUpdatedConsumer;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Starting {nameof(DesktopBridgeHost)}");

        _applicationLifetime.ApplicationStarted.Register(OnStarted);
        _applicationLifetime.ApplicationStopping.Register(OnStopped);

        MarkAsReady();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Stopping {nameof(BridgeServer)}...");
        _bridgeServer.Stop();
        return Task.CompletedTask;
    }

    private void MarkAsReady()
    {
        File.Create("/tmp/healthy");
    }

    private void OnStarted()
    {
        _logger.LogInformation($"Starting {nameof(BridgeServer)}");
        _bridgeServer.Start();
        _geographicalAreaUpdatedConsumer.Consume();
    }

    private void OnStopped()
    {
        _geographicalAreaUpdatedConsumer.Dispose();
        _bridgeServer.Dispose();
        _logger.LogInformation("Stopped");
    }
}
