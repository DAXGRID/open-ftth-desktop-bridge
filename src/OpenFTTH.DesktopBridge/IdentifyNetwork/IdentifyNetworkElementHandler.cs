using MediatR;
using OpenFTTH.DesktopBridge.Bridge;
using System;
using System.Text;
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
            var base64JsonString = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Message));
            _bridgeServer.MulticastText(base64JsonString);

            return await Task.FromResult(new Unit());
        }
    }
}
