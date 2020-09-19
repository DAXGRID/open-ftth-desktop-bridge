using MediatR;
using OpenFTTH.Events.Geo;
using OpenFTTH.DesktopBridge.Bridge;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenFTTH.DesktopBridge.GeographicalAreaUpdated
{
    public class GeographicalAreaUpdated : IRequest<Unit>
    {
        public ObjectsWithinGeographicalAreaUpdated ObjectsWithinGeographicalAreaUpdated { get; }

        public GeographicalAreaUpdated(ObjectsWithinGeographicalAreaUpdated objectsWithinGeographicalAreaUpdated)
        {
            ObjectsWithinGeographicalAreaUpdated = objectsWithinGeographicalAreaUpdated;
        }
    }

    public class GeographicalAreaUpdatedHandler : IRequestHandler<GeographicalAreaUpdated>
    {
        private readonly BridgeServer _bridgeServer;

        public GeographicalAreaUpdatedHandler(BridgeServer bridgeServer)
        {
            _bridgeServer = bridgeServer;
        }

        public async Task<Unit> Handle(GeographicalAreaUpdated request, CancellationToken cancellationToken)
        {
            _bridgeServer.MulticastText(JsonConvert.SerializeObject(request.ObjectsWithinGeographicalAreaUpdated));
            return await Task.FromResult(new Unit());
        }
    }
}
