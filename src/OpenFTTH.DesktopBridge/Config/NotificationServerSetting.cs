namespace OpenFTTH.DesktopBridge.Config;

public sealed record NotificationServerSetting
{
    public string Domain { get; init; }
    public int Port { get; init; }
}
