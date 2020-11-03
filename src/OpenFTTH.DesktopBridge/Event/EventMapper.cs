using System;
using OpenFTTH.DesktopBridge.IdentifyNetwork;
using OpenFTTH.DesktopBridge.Retrieve;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MediatR;

namespace OpenFTTH.DesktopBridge.Event
{
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
                case "RetrieveSelected":
                    return JsonConvert.DeserializeObject<RetrieveSelected>(jsonEvent);
                default:
                    throw new ArgumentException($"No event of type '{eventType}'");
            }
        }
    }
}
