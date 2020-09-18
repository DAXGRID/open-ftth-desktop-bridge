using System.Net;
using Microsoft.Extensions.Logging;

namespace OpenFTTH.DesktopBridge.Bridge
{
    public class BridgeServerFactory : IBridgeServerFactory
    {
        private readonly ILogger _logger;

        public BridgeServerFactory(ILogger<BridgeServer> logger)
        {
            _logger = logger;
        }

        public BridgeServer Create(int port)
        {
            var bridgeServer = new BridgeServer(IPAddress.Any, port, _logger);
            bridgeServer.OptionKeepAlive = true;

            return bridgeServer;
        }
    }
}
