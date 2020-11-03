using MediatR;
using System.Threading;
using System.Threading.Tasks;
using OpenFTTH.DesktopBridge.Bridge;
using Newtonsoft.Json;

namespace OpenFTTH.DesktopBridge.Retrieve
{
    public class RetrieveSelectedResponseHandler : IRequestHandler<RetrieveSelectedResponse>
    {
        private readonly IBridgeServer _bridgeServer;

        public RetrieveSelectedResponseHandler(IBridgeServer bridgeServer)
        {
            _bridgeServer = bridgeServer;
        }

        public async Task<Unit> Handle(RetrieveSelectedResponse request, CancellationToken cancellationToken)
        {
            _bridgeServer.MulticastText(JsonConvert.SerializeObject(request));
            return await Task.FromResult(new Unit());
        }
    }
}
