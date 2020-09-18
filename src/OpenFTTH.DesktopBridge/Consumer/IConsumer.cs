using System;

namespace OpenFTTH.DesktopBridge.Consumer
{
    public interface IConsumer : IDisposable
    {
        void Subscribe();
    }
}
