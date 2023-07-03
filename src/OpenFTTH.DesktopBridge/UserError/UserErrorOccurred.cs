using MediatR;

namespace OpenFTTH.DesktopBridge.UserError;

public record UserErrorOccurred : IRequest<Unit>
{
    public string EventType { get; init; }
    public string ErrorCode { get; init; }
    public string Username { get; init; }

    public UserErrorOccurred(
        string eventType,
        string errorCode,
        string username)
    {
        EventType = eventType;
        ErrorCode = errorCode;
        Username = username;
    }
}
