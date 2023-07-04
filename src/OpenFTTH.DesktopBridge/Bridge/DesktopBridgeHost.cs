using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenFTTH.DesktopBridge.GeographicalAreaUpdated;
using OpenFTTH.DesktopBridge.Notification;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace OpenFTTH.DesktopBridge.Bridge;

public class DesktopBridgeHost : BackgroundService
{
    private readonly ILogger<DesktopBridgeHost> _logger;
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IBridgeServer _bridgeServer;
    private readonly INotificationConsumer _notificationConsumer;

    public DesktopBridgeHost(
        ILogger<DesktopBridgeHost> logger,
        IHostApplicationLifetime applicationLifetime,
        IBridgeServer bridgeServer,
        INotificationConsumer notificationConsumer)
    {
        _logger = logger;
        _applicationLifetime = applicationLifetime;
        _bridgeServer = bridgeServer;
        _notificationConsumer = notificationConsumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting {Name}.", nameof(BridgeServer));
        if (!_bridgeServer.Start())
        {
            throw new InvalidDataException($"Could not start {nameof(BridgeServer)}");
        }

        _logger.LogInformation("Starting {Name}.", nameof(INotificationConsumer));
        var notificationConsumer = _notificationConsumer
            .Consume()
            .ConfigureAwait(false);

        // Mark service as ready.
        using var _ = File.Create(Path.Combine(Path.GetTempPath(), "healthy"));

        _logger.LogInformation("Service is now healthy.");

        await notificationConsumer;
    }

    public override void Dispose()
    {
        _notificationConsumer.Dispose();
        _bridgeServer.Stop();
        base.Dispose();
    }
}
