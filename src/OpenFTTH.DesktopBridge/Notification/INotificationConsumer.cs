using System;
using System.Threading.Tasks;

namespace OpenFTTH.DesktopBridge.Notification;

public interface INotificationConsumer : IDisposable
{
    Task Consume();
}
