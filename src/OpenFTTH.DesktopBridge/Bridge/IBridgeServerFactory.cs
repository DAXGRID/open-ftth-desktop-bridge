namespace OpenFTTH.DesktopBridge.Bridge
{
    public interface IBridgeServerFactory
    {
        BridgeServer Create(int port);
    }
}
