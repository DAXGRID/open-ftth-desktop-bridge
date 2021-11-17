using System.Net;
using System.Net.Sockets;
using NetCoreServer;
using Microsoft.Extensions.Logging;
using MediatR;
using OpenFTTH.DesktopBridge.Event;
using System.Threading;

namespace OpenFTTH.DesktopBridge.Bridge;

public class BridgeServer : WsServer, IBridgeServer
{
    private readonly ILogger<BridgeServer> _logger;
    private readonly IMediator _mediator;
    private readonly IEventMapper _eventMapper;
    private Timer _timer;

    public BridgeServer(IPAddress address,
                        int port,
                        ILogger<BridgeServer> logger,
                        IMediator mediator,
                        IEventMapper eventMapper) : base(address, port)
    {
        _logger = logger;
        _mediator = mediator;
        _eventMapper = eventMapper;
        OptionKeepAlive = true;
    }

    protected override TcpSession CreateSession()
    {
        return new BridgeSession(this, _logger, _mediator, _eventMapper);
    }

    protected override void OnStarted()
    {
        base.OnStarted();
        _timer = new Timer(o => Ping(), null, 0, 30000);
    }

    protected override void OnError(SocketError error)
    {
        _logger.LogError($"Chat WebSocket server caught an error with code {error}");
    }

    protected override void OnStopped()
    {
        _timer.Dispose();
    }

    private void Ping()
    {
        _logger.LogDebug("Pinging clients");
        SendPing("ping");
    }
}
