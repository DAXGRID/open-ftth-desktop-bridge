using System;
using Topos.Config;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using OpenFTTH.DesktopBridge.Config;
using OpenFTTH.Events.Geo;
using MediatR;

namespace OpenFTTH.DesktopBridge.GeographicalAreaUpdated
{
    public class GeographicalAreaUpdatedKafkaConsumer : IGeographicalAreaUpdatedConsumer, IDisposable
    {
        private IDisposable _consumer;
        private readonly KafkaSetting _kafkaSetting;
        private readonly ILogger<GeographicalAreaUpdatedKafkaConsumer> _logger;
        private readonly IMediator _mediator;

        public GeographicalAreaUpdatedKafkaConsumer(
            IOptions<KafkaSetting> kafkaSetting,
            ILogger<GeographicalAreaUpdatedKafkaConsumer> logger,
            IMediator mediator)
        {
            _kafkaSetting = kafkaSetting.Value;
            _logger = logger;
            _mediator = mediator;
        }

        public void Consume()
        {
            _logger.LogInformation($"Starting consuming on server {_kafkaSetting.Server} with topic: '{_kafkaSetting.NotificationGeographicalAreaUpdated}' and consumer group: '{_kafkaSetting.NotificationGeographicalAreaUpdatedConsumer}'");

            _consumer = Configure
                .Consumer(_kafkaSetting.NotificationGeographicalAreaUpdatedConsumer, c => c.UseKafka(_kafkaSetting.Server))
                .Serialization(s => s.UseNewtonsoftJson())
                .Topics(t => t.Subscribe(_kafkaSetting.NotificationGeographicalAreaUpdated))
                .Logging(x => x.UseSerilog())
                .Positions(x =>
                {
                    x.SetInitialPosition(StartFromPosition.Now);
                    x.StoreInMemory();
                })
                .Options(x => { x.SetMinimumBatchSize(1); })
                .Handle(async (messages, context, token) =>
                {
                    foreach (var message in messages)
                    {
                        _logger.LogInformation($"Received message with position {message.Position}");

                        switch (message.Body)
                        {
                            case ObjectsWithinGeographicalAreaUpdated objectsWithinGeographicalAreaUpdated:
                                await _mediator.Send(new GeographicalAreaUpdated(objectsWithinGeographicalAreaUpdated));
                                break;
                        }
                    }
                }).Start();
        }

        public void Dispose()
        {
            _consumer.Dispose();
        }
    }
}
