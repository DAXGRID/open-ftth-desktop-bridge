using MediatR;

namespace OpenFTTH.DesktopBridge.UserError;

public record UserErrorOccurred : IRequest<Unit>
{
    public string EventType { get; private set; } = nameof(UserErrorOccurred);
    public string ErrorCode { get; init; }
    public string Username { get; init; }

    public UserErrorOccurred(
        string errorCode,
        string username)
    {
        ErrorCode = errorCode;
        Username = username;
    }
}
