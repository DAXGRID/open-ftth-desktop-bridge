using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OpenFTTH.DesktopBridge.Config;
using OpenFTTH.DesktopBridge.UserError;
using OpenFTTH.Events.Geo;
using OpenFTTH.NotificationClient;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace OpenFTTH.DesktopBridge.Notification;

public sealed class NotificationConsumer : INotificationConsumer
{
    private readonly IMediator _mediator;
    private readonly Client _notificationClient;
    private readonly ILogger<NotificationConsumer> _logger;

    public NotificationConsumer(
        IOptions<NotificationServerSetting> notificationServerSetting,
        IMediator mediator,
        ILogger<NotificationConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;

        var ipAddress = Dns.GetHostEntry(notificationServerSetting.Value.Domain).AddressList
                .First(x => x.AddressFamily == AddressFamily.InterNetwork);

        _notificationClient = new Client(
            ipAddress: ipAddress,
            port: notificationServerSetting.Value.Port);
    }

    public async Task Consume()
    {
        var notificationReaderCh = _notificationClient.Connect();

        var notifications = notificationReaderCh
            .ReadAllAsync()
            .ConfigureAwait(false);

        await foreach (var notification in notifications)
        {
            if (string.Equals(notification.Type, "GeographicalAreaUpdated", StringComparison.OrdinalIgnoreCase))
            {
                var areaUpdated = JsonConvert
                    .DeserializeObject<ObjectsWithinGeographicalAreaUpdated>(notification.Body);

                if (areaUpdated is null)
                {
                    throw new InvalidOperationException(
                        $"Deserializeing of {nameof(GeographicalAreaUpdated)} resulted in null.");
                }

                await _mediator
                    .Send(new GeographicalAreaUpdated.GeographicalAreaUpdated(areaUpdated))
                    .ConfigureAwait(false);
            }
            else if (string.Equals(notification.Type, "UserErrorOccurred", StringComparison.OrdinalIgnoreCase))
            {
                var userErrorOccurred = JsonConvert
                    .DeserializeObject<UserErrorOccurred>(notification.Body);

                if (userErrorOccurred is null)
                {
                    throw new InvalidOperationException(
                        $"Deserializeing of {nameof(UserErrorOccurred)} resulted in null.");
                }

                await _mediator
                    .Send(userErrorOccurred)
                    .ConfigureAwait(false);
            }
            else
            {
                // This is okay, we do not care about all notifications.
                _logger.LogDebug(
                    "Received message with {Type} that could not be handled",
                    notification.Type);
            }
        }
    }

    public void Dispose()
    {
        _notificationClient.Dispose();
    }
}
