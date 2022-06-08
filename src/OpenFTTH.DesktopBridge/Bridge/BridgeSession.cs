using MediatR;
using Microsoft.Extensions.Logging;
using NetCoreServer;
using OpenFTTH.DesktopBridge.Event;
using System;
using System.Net.Sockets;
using System.Text;

namespace OpenFTTH.DesktopBridge.Bridge;

public class BridgeSession : WsSession
{
    private readonly ILogger<BridgeServer> _logger;
    private readonly IMediator _mediator;
    private readonly IEventMapper _eventMapper;

    public BridgeSession(WsServer server,
                         ILogger<BridgeServer> logger,
                         IMediator mediator,
                         IEventMapper eventMapper) : base(server)
    {
        _logger = logger;
        _mediator = mediator;
        _eventMapper = eventMapper;
    }

    public override void OnWsConnected(HttpRequest request)
    {
        _logger.LogInformation($"WebSocket session with Id {Id} connected!");
    }

    public override void OnWsDisconnected()
    {
        _logger.LogInformation($"WebSocket session with Id {Id} disconnected!");
    }

    public override void OnWsReceived(byte[] buffer, long offset, long size)
    {
        var jsonMessage = string.Empty;
        try
        {
            jsonMessage = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            _logger.LogDebug($"Received from id: '{Id}': {jsonMessage}");

            var eventMessage = _eventMapper.Map(jsonMessage);

            _mediator.Send(eventMessage).Wait();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Received invalid message with content: {jsonMessage} and exception: {ex.Message}");
        }
    }

    protected override void OnError(SocketError error)
    {
        _logger.LogError($"WebSocket session caught an error with code {error}");
    }
}
