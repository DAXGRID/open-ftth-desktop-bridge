using System.Collections.Generic;
using MediatR;

namespace OpenFTTH.DesktopBridge.Highlight
{
    public class SelectRouteSegments : IRequest<Unit>
    {
        public string EventType { get; set; }
        public IEnumerable<string> Mrids { get; set; }
        public string Username { get; set; }
    }
}
