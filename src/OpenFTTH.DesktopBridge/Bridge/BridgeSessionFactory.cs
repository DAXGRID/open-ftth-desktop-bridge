using NetCoreServer;
using Microsoft.Extensions.Logging;

namespace OpenFTTH.DesktopBridge.Bridge
{
    public class BridgeSessionFactory : IBridgeSessionFactory
    {
        private readonly ILogger<BridgeSession> _bridgeLogging;

        public BridgeSessionFactory(ILogger<BridgeSession> bridgeLogging)
        {
            _bridgeLogging = bridgeLogging;
        }

        public BridgeSession Create(WsServer wsServer)
        {
            return new BridgeSession(wsServer, _bridgeLogging);
        }
    }
}
