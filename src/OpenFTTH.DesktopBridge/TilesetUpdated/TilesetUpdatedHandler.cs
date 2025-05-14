using MediatR;
using Newtonsoft.Json;
using OpenFTTH.DesktopBridge.Bridge;
using System.Threading;
using System.Threading.Tasks;

namespace OpenFTTH.DesktopBridge.TilesetUpdated;

public record TilesetUpdated : IRequest
{
    public string EventType { get; private set; } = nameof(TilesetUpdated);
    public string TilesetName { get; init; }

    public TilesetUpdated(string tilesetName)
    {
        TilesetName = tilesetName;
    }
}

public class TilesetUpdatedHandler : IRequestHandler<TilesetUpdated>
{
    private readonly IBridgeServer _bridgeServer;

    public TilesetUpdatedHandler(IBridgeServer bridgeServer)
    {
        _bridgeServer = bridgeServer;
    }

    public async Task<Unit> Handle(
        TilesetUpdated request,
        CancellationToken cancellationToken)
    {
        _bridgeServer.MulticastText(JsonConvert.SerializeObject(request));
        return await Task.FromResult(new Unit());
    }
}
