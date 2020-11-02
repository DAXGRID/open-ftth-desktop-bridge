using System.Net.Sockets;
using System.Text;
using NetCoreServer;
using Microsoft.Extensions.Logging;
using MediatR;
using Newtonsoft.Json.Linq;
using OpenFTTH.DesktopBridge.IdentifyNetwork;
using OpenFTTH.DesktopBridge.Retrieve;

namespace OpenFTTH.DesktopBridge.Bridge
{
    public class BridgeSession : WsSession
    {
        private readonly ILogger<BridgeServer> _logger;
        private readonly IMediator _mediator;

        public BridgeSession(WsServer server, ILogger<BridgeServer> logger, IMediator mediator) : base(server)
        {
            _logger = logger;
            _mediator = mediator;
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

                var message = JObject.Parse(jsonMessage);
                var eventTypePropertyName = "eventType";

                var eventType = message.GetValue(eventTypePropertyName)?.ToString();

                if (string.IsNullOrEmpty(eventType))
                {
                    _logger.LogWarning($"The following message: '{jsonMessage}', does not contain a property named '{eventTypePropertyName}' and cannot be parsed.");
                    return;
                }

                switch (eventType)
                {
                    case "IdentifyNetworkElement":
                        await _mediator.Send(new IdentifyNetworkElement(jsonMessage));
                        break;
                    case "RetriveSelected":
                        await _mediator.Send(new RetrieveSelected("INSERT USERNAME"));
                        break;
                    default:
                        _logger.LogWarning($"No event of type '{eventType}'");
                        break;
                }
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
