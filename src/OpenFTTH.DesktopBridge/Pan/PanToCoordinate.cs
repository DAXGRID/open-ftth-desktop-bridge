using MediatR;

namespace OpenFTTH.DesktopBridge.Pan
{
    public class PanToCoordinate : IRequest<Unit>
    {
        public string EventType { get; set; }
        public string Username { get; set; }
        public float[] Coordinate { get; set; }
    }
}
