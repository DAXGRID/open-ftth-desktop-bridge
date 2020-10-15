using MediatR;
using OpenFTTH.DesktopBridge.Bridge;
using System.Threading;
using System.Threading.Tasks;

namespace OpenFTTH.DesktopBridge.IdentifyNetwork
{
    public class IdentifyNetworkElement : IRequest<Unit>
    {
        public string Message { get; }

        public IdentifyNetworkElement(string message)
        {
            Message = message;
        }
    }

    public class IdentifyNetworkElementHandler : IRequestHandler<IdentifyNetworkElement>
    {
        private readonly IBridgeServer _bridgeServer;

        public IdentifyNetworkElementHandler(IBridgeServer bridgeServer)
        {
            _bridgeServer = bridgeServer;
        }

        public async Task<Unit> Handle(IdentifyNetworkElement request, CancellationToken cancellationToken)
        {
            _bridgeServer.MulticastText(request.Message);

            return await Task.FromResult(new Unit());
        }
    }
}
