using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace OpenFTTH.DesktopBridge.Bridge
{
    public class DesktopBridgeHost : IHostedService
    {
        private readonly ILogger<DesktopBridgeHost> _logger;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private BridgeServer _bridgeServer;

        public DesktopBridgeHost(ILogger<DesktopBridgeHost> logger, IHostApplicationLifetime applicationLifetime, BridgeServer bridgeServer)
        {
            _logger = logger;
            _applicationLifetime = applicationLifetime;
            _bridgeServer = bridgeServer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting");

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
            _logger.LogInformation($"Starting {nameof(BridgeServer)} on port 5000");
            _bridgeServer.Start();
            _bridgeServer.OptionKeepAlive = true;
        }

        private void OnStopped()
        {
            _bridgeServer.Dispose();
            _logger.LogInformation("Stopped");
        }
    }
}
