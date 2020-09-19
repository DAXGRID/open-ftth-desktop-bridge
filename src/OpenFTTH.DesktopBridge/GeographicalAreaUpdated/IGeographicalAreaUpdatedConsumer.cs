using System;

namespace OpenFTTH.DesktopBridge.GeographicalAreaUpdated
{
    public interface IGeographicalAreaUpdatedConsumer : IDisposable
    {
        void Consume();
    }
}
