using System.Net.Sockets;
using System.Text;
using NetCoreServer;
using Microsoft.Extensions.Logging;

namespace OpenFTTH.DesktopBridge.Bridge
{
    public class BridgeSession : WsSession
    {
        private readonly ILogger _logger;

        public BridgeSession(WsServer server, ILogger logger) : base(server)
        {
            _logger = logger;
        }

        public override void OnWsConnected(HttpRequest request)
        {
            _logger.LogInformation($"Chat WebSocket session with Id {Id} connected!");
        }

        public override void OnWsDisconnected()
        {
            _logger.LogInformation($"Chat WebSocket session with Id {Id} disconnected!");
        }

        public override void OnWsReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            ((WsServer)Server).MulticastText(message);
        }

        protected override void OnError(SocketError error)
        {
            _logger.LogError($"Chat WebSocket session caught an error with code {error}");
        }
    }
}
