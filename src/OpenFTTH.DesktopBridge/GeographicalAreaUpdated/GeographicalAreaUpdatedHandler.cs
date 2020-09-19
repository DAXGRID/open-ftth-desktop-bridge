using MediatR;
using OpenFTTH.Events.Geo;
using OpenFTTH.DesktopBridge.Bridge;
using System.Threading;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

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
            var json = JsonConvert.SerializeObject(request.ObjectsWithinGeographicalAreaUpdated);
            var base64JsonString = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            _bridgeServer.MulticastText(base64JsonString);
            return await Task.FromResult(new Unit());
        }
    }
}
