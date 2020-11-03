using MediatR;

namespace OpenFTTH.DesktopBridge.IdentifyNetwork
{
    public class IdentifyNetworkElement : IRequest<Unit>
    {
        public string EventType { get; set; }
        public int IdentifiedFeatureId { get; set; }
        public string SelectedType { get; set; }
        public string Username { get; set; }
    }
}
