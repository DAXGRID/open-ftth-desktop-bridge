using System.Net;
using System.Net.Sockets;
using NetCoreServer;
using Microsoft.Extensions.Logging;

namespace OpenFTTH.DesktopBridge.Bridge
{
    public class BridgeServer : WsServer
    {
        private readonly ILogger _logger;

        public BridgeServer(IPAddress address, int port, ILogger logger) : base(address, port)
        {
            _logger = logger;
        }

        protected override TcpSession CreateSession()
        {
            return new BridgeSession(this, _logger);
        }

        protected override void OnError(SocketError error)
        {
            _logger.LogError($"Chat WebSocket server caught an error with code {error}");
        }
    }
}
