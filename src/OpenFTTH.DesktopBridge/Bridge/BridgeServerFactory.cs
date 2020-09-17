using System.Net;

namespace OpenFTTH.DesktopBridge.Bridge
{
    public class BridgeServerFactory : IBridgeServerFactory
    {
        public BridgeServer Create(int port)
        {
            return new BridgeServer(IPAddress.Any, port);
        }
    }
}
