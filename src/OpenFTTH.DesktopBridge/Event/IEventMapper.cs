using MediatR;

namespace OpenFTTH.DesktopBridge.Event
{
    public interface IEventMapper
    {
        IRequest<Unit> Map(string jsonEvent);
    }
}
