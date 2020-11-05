using MediatR;
using System.Threading;
using System.Threading.Tasks;
using OpenFTTH.DesktopBridge.Bridge;
using Newtonsoft.Json;

namespace OpenFTTH.DesktopBridge.Retrieve
{
    public class RetrieveSelectedHandler : IRequestHandler<RetrieveSelected>
    {
        private readonly IBridgeServer _bridgeServer;

        public RetrieveSelectedHandler(IBridgeServer bridgeServer)
        {
            _bridgeServer = bridgeServer;
        }

        public async Task<Unit> Handle(RetrieveSelected request, CancellationToken cancellationToken)
        {
            _bridgeServer.MulticastText(JsonConvert.SerializeObject(request));
            return await Task.FromResult(new Unit());
        }
    }
}
