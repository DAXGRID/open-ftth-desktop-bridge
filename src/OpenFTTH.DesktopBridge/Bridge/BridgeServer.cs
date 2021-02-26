using System.Net;
using System.Net.Sockets;
using NetCoreServer;
using Microsoft.Extensions.Logging;
using MediatR;
using OpenFTTH.DesktopBridge.Event;
using System;
using System.Threading;

namespace OpenFTTH.DesktopBridge.Bridge
{
    public class BridgeServer : WsServer, IBridgeServer, IDisposable
    {
        private readonly ILogger<BridgeServer> _logger;
        private readonly IMediator _mediator;
        private readonly IEventMapper _eventMapper;

        public BridgeServer(IPAddress address, int port, ILogger<BridgeServer> logger, IMediator mediator, IEventMapper eventMapper) : base(address, port)
        {
            _logger = logger;
            _mediator = mediator;
            _eventMapper = eventMapper;
            OptionKeepAlive = true;
        }

        protected override TcpSession CreateSession()
        {
            var t = new Timer(o => Ping(), null, 0, 30000);
            return new BridgeSession(this, _logger, _mediator, _eventMapper);
        }

        protected override void OnError(SocketError error)
        {
            _logger.LogError($"Chat WebSocket server caught an error with code {error}");
        }

        private void Ping()
        {
            SendPing("ping");
        }
    }
}
