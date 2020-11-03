using MediatR;

namespace OpenFTTH.DesktopBridge.Retrieve
{
    public class RetrieveSelected : IRequest<Unit>
    {
        public string Username { get; set; }
        public string EventType { get; set; }
    }
}
