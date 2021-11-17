using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using OpenFTTH.DesktopBridge.Bridge;

namespace OpenFTTH.DesktopBridge.Highlight;

public class SelectRouteSegmentsHandler : IRequestHandler<SelectRouteSegments>
{
    private readonly IBridgeServer _bridgeServer;

    public SelectRouteSegmentsHandler(IBridgeServer bridgeServer)
    {
        _bridgeServer = bridgeServer;
    }

    public async Task<Unit> Handle(SelectRouteSegments request, CancellationToken cancellationToken)
    {
        _bridgeServer.MulticastText(JsonConvert.SerializeObject(request));
        return await Task.FromResult(new Unit());
    }
}
