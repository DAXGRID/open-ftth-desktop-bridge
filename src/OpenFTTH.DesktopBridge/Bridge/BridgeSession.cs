using System.Net.Sockets;
using System.Text;
using NetCoreServer;
using Microsoft.Extensions.Logging;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenFTTH.DesktopBridge.IdentifyNetwork;
using OpenFTTH.DesktopBridge.Retrieve;
using OpenFTTH.DesktopBridge.Event;

namespace OpenFTTH.DesktopBridge.Bridge
{
    public class BridgeSession : WsSession
    {
        private readonly ILogger<BridgeServer> _logger;
        private readonly IMediator _mediator;
        private readonly IEventMapper _eventMapper;

        public BridgeSession(WsServer server, ILogger<BridgeServer> logger, IMediator mediator, IEventMapper eventMapper) : base(server)
        {
            _logger = logger;
            _mediator = mediator;
            _eventMapper = eventMapper;
        }

        public override void OnWsConnected(HttpRequest request)
        {
            _logger.LogInformation($"Chat WebSocket session with Id {Id} connected!");
        }

        public override void OnWsDisconnected()
        {
            _logger.LogInformation($"Chat WebSocket session with Id {Id} disconnected!");
        }

        public async override void OnWsReceived(byte[] buffer, long offset, long size)
        {
            var jsonMessage = string.Empty;
            try
            {
                jsonMessage = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
                _logger.LogDebug($"Received from id: '{Id}': {jsonMessage}");
                var eventMessage = _eventMapper.Map(jsonMessage);

                await _mediator.Send(eventMessage);
            }
            catch
            {
                _logger.LogError($"Received invalid message with content: {jsonMessage}");
            }
        }

        protected override void OnError(SocketError error)
        {
            _logger.LogError($"Chat WebSocket session caught an error with code {error}");
        }
    }
}
