using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OpenFTTH.DesktopBridge.Config;
using OpenFTTH.Events.Geo;
using OpenFTTH.NotificationClient;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace OpenFTTH.DesktopBridge.GeographicalAreaUpdated;

public class GeographicalAreaUpdatedConsumer : IGeographicalAreaUpdatedConsumer
{
    private readonly IMediator _mediator;
    private readonly Client _notificationClient;

    public GeographicalAreaUpdatedConsumer(
        IOptions<NotificationServerSetting> notificationServerSetting,
        IMediator mediator)
    {
        _mediator = mediator;

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
            if (string.CompareOrdinal(notification.Type, "GeographicalAreaUpdated") == 0)
            {
                var areaUpdated = JsonConvert
                    .DeserializeObject<ObjectsWithinGeographicalAreaUpdated>(notification.Body);

                if (areaUpdated is null)
                {
                    throw new InvalidOperationException(
                        $"Deserializeing of {nameof(GeographicalAreaUpdated)} resulted in null.");
                }

                await _mediator
                    .Send(new GeographicalAreaUpdated(areaUpdated))
                    .ConfigureAwait(false);
            }
        }
    }

    public void Dispose()
    {
        _notificationClient.Dispose();
    }
}
