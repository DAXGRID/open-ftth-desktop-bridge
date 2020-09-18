using System.Net;
using System.Net.Sockets;
using NetCoreServer;
using Microsoft.Extensions.Logging;

namespace OpenFTTH.DesktopBridge.Bridge
{
    public class BridgeServer : WsServer
    {
        private readonly ILogger<BridgeServer> _logger;
        private readonly IBridgeSessionFactory _bridgeSessionFactory;

        public BridgeServer(IPAddress address, int port, IBridgeSessionFactory bridgeSessionFactory, ILogger<BridgeServer> logger) : base(address, port)
        {
            _logger = logger;
            _bridgeSessionFactory = bridgeSessionFactory;
        }

        protected override TcpSession CreateSession()
        {
            return _bridgeSessionFactory.Create(this);
        }

        protected override void OnError(SocketError error)
        {
            _logger.LogError($"Chat WebSocket server caught an error with code {error}");
        }
    }
}
