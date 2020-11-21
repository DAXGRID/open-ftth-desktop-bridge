using MediatR;
using OpenFTTH.DesktopBridge.Bridge;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenFTTH.DesktopBridge.Pan
{
    public class PanToCoordinateHandler : IRequestHandler<PanToCoordinate>
    {
        private readonly IBridgeServer _bridgeServer;

        public PanToCoordinateHandler(IBridgeServer bridgeServer)
        {
            _bridgeServer = bridgeServer;
        }

        public async Task<Unit> Handle(PanToCoordinate request, CancellationToken cancellationToken)
        {
            _bridgeServer.MulticastText(JsonConvert.SerializeObject(request));
            return await Task.FromResult(new Unit());
        }
    }
}
