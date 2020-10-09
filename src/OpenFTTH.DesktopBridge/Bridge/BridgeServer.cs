using System.Net;
using System.Net.Sockets;
using NetCoreServer;
using Microsoft.Extensions.Logging;
using MediatR;

namespace OpenFTTH.DesktopBridge.Bridge
{
    public class BridgeServer : WsServer, IBridgeServer
    {
        private readonly ILogger<BridgeServer> _logger;
        private readonly IMediator _mediator;

        public BridgeServer(IPAddress address, int port, ILogger<BridgeServer> logger, IMediator mediator) : base(address, port)
        {
            _logger = logger;
            _mediator = mediator;
            OptionKeepAlive = true;
        }

        protected override TcpSession CreateSession()
        {
            return new BridgeSession(this, _logger, _mediator);
        }

        protected override void OnError(SocketError error)
        {
            _logger.LogError($"Chat WebSocket server caught an error with code {error}");
        }
    }
}
