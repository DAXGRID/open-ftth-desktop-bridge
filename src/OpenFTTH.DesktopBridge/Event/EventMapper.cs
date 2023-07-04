using System;
using OpenFTTH.DesktopBridge.IdentifyNetwork;
using OpenFTTH.DesktopBridge.Retrieve;
using OpenFTTH.DesktopBridge.Pan;
using OpenFTTH.DesktopBridge.Highlight;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MediatR;
using OpenFTTH.DesktopBridge.UserError;

namespace OpenFTTH.DesktopBridge.Event;

public class EventMapper : IEventMapper
{
    public IRequest<Unit> Map(string jsonEvent)
    {
        var message = JObject.Parse(jsonEvent);

        var eventTypePropertyName = "eventType";
        var eventType = message.GetValue(eventTypePropertyName)?.ToString();

        switch (eventType)
        {
            case "IdentifyNetworkElement":
                return JsonConvert.DeserializeObject<IdentifyNetworkElement>(jsonEvent);
            case "RetrieveIdentifiedNetworkElement":
                return JsonConvert.DeserializeObject<RetrieveIdentifiedNetworkElement>(jsonEvent);
            case "RetrieveSelected":
                return JsonConvert.DeserializeObject<RetrieveSelected>(jsonEvent);
            case "RetrieveSelectedResponse":
                return JsonConvert.DeserializeObject<RetrieveSelectedResponse>(jsonEvent);
            case "PanToCoordinate":
                return JsonConvert.DeserializeObject<PanToCoordinate>(jsonEvent);
            case "HighlightFeatures":
                return JsonConvert.DeserializeObject<HighlightFeatures>(jsonEvent);
            case "SelectRouteSegments":
                return JsonConvert.DeserializeObject<SelectRouteSegments>(jsonEvent);
            default:
                throw new ArgumentException($"No event of type '{eventType}'");
        }
    }
}
