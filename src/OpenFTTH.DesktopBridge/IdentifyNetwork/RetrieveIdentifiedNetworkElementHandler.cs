using MediatR;
using OpenFTTH.DesktopBridge.Bridge;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenFTTH.DesktopBridge.IdentifyNetwork;

public class RetrieveIdentifiedNetworkElementhandler : IRequestHandler<RetrieveIdentifiedNetworkElement>
{
    private readonly IBridgeServer _bridgeServer;

    public RetrieveIdentifiedNetworkElementhandler(IBridgeServer bridgeServer)
    {
        _bridgeServer = bridgeServer;
    }

    public async Task<Unit> Handle(RetrieveIdentifiedNetworkElement request, CancellationToken cancellationToken)
    {
        _bridgeServer.MulticastText(JsonConvert.SerializeObject(request));
        return await Task.FromResult(new Unit());
    }
}
