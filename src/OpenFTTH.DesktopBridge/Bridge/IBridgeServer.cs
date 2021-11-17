using System;

namespace OpenFTTH.DesktopBridge.Bridge;

public interface IBridgeServer : IDisposable
{
    bool MulticastText(string text);
    bool Start();
    bool Stop();
}
