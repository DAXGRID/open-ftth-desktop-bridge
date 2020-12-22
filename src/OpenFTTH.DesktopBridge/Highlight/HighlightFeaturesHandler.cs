using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using OpenFTTH.DesktopBridge.Bridge;

namespace OpenFTTH.DesktopBridge.Highlight
{
    public class HighlightFeaturesHandler : IRequestHandler<HighlightFeatures>
    {
        private readonly IBridgeServer _bridgeServer;

        public HighlightFeaturesHandler(IBridgeServer bridgeServer)
        {
            _bridgeServer = bridgeServer;
        }

        public async Task<Unit> Handle(HighlightFeatures request, CancellationToken cancellationToken)
        {
            _bridgeServer.MulticastText(JsonConvert.SerializeObject(request));
            return await Task.FromResult(new Unit());
        }
    }
}
