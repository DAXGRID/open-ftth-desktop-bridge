using System;
using System.Threading.Tasks;

namespace OpenFTTH.DesktopBridge.GeographicalAreaUpdated;

public interface IGeographicalAreaUpdatedConsumer : IDisposable
{
    Task Consume();
}
