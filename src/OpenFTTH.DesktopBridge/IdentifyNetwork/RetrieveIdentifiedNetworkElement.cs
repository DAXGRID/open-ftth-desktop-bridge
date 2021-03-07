using MediatR;

namespace OpenFTTH.DesktopBridge.IdentifyNetwork
{
    public class RetrieveIdentifiedNetworkElement : IRequest<Unit>
    {
        public string EventType { get; set; }
        public string Username { get; set; }
    }
}
