using MediatR;

namespace OpenFTTH.DesktopBridge.Retrieve
{
    public class RetrieveSelected : IRequest
    {
        public string EventType { get; set; }
        public string Username { get; set; }
    }
}
