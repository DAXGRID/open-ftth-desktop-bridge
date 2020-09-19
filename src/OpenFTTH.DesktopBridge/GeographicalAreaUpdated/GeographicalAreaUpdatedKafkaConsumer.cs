using System;
using Topos.Config;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using OpenFTTH.DesktopBridge.Config;
using OpenFTTH.Events.Geo;

namespace OpenFTTH.DesktopBridge.GeographicalAreaUpdated
{
    public class GeographicalAreaUpdatedKafkaConsumer : IGeographicalAreaUpdatedConsumer, IDisposable
    {
        private IDisposable _consumer;
        private KafkaSetting _kafkaSetting;
        private readonly ILogger<GeographicalAreaUpdatedKafkaConsumer> _logger;

        public GeographicalAreaUpdatedKafkaConsumer(IOptions<KafkaSetting> kafkaSetting, ILogger<GeographicalAreaUpdatedKafkaConsumer> logger)
        {
            _kafkaSetting = kafkaSetting.Value;
            _logger = logger;
        }

        public void Consume()
        {
            _logger.LogInformation($"Starting consuming on server {_kafkaSetting.Server} with topic: '{_kafkaSetting.EventGeographicalAreaUpdated}' and consumer group: '{_kafkaSetting.EventGeographicalAreaUpdatedConsumer}'");

            _consumer = Configure
                .Consumer(_kafkaSetting.EventGeographicalAreaUpdatedConsumer, c => c.UseKafka(_kafkaSetting.Server))
                .Serialization(s => s.UseNewtonsoftJson())
                .Topics(t => t.Subscribe(_kafkaSetting.EventGeographicalAreaUpdated))
                .Logging(x => x.UseSerilog())
                .Positions(p => p.StoreInFileSystem(_kafkaSetting.PositionFilePath))
                .Handle(async (messages, context, token) =>
                {
                    foreach (var message in messages)
                    {
                        _logger.LogInformation($"Received message with position {message.Position}");

                        switch (message.Body)
                        {
                            case ObjectsWithinGeographicalAreaUpdated objectsWithinGeographicalAreaUpdated:
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
