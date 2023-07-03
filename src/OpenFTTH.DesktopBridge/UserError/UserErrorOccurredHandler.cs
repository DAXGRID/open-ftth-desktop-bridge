using MediatR;
using Newtonsoft.Json;
using OpenFTTH.DesktopBridge.Bridge;
using System.Threading;
using System.Threading.Tasks;

namespace OpenFTTH.DesktopBridge.UserError;

public class UserErrorOccurredHandler : IRequestHandler<UserErrorOccurred>
{
    private readonly IBridgeServer _bridgeServer;

    public UserErrorOccurredHandler(IBridgeServer bridgeServer)
    {
        _bridgeServer = bridgeServer;
    }

    public async Task<Unit> Handle(
        UserErrorOccurred request,
        CancellationToken cancellationToken)
    {
        _bridgeServer.MulticastText(JsonConvert.SerializeObject(request));
        return await Task.FromResult(new Unit());
    }
}
