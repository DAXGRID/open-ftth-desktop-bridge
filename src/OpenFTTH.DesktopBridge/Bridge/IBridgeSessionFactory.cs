using NetCoreServer;

namespace OpenFTTH.DesktopBridge.Bridge
{
    public interface IBridgeSessionFactory
    {
        BridgeSession Create(WsServer wsServer);
    }
}
