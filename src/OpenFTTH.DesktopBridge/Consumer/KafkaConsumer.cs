using System;
using Topos.Config;
using OpenFTTH.DesktopBridge.Config;

namespace OpenFTTH.DesktopBridge.Consumer
{
    public class KafkaConsumer : IConsumer, IDisposable
    {
        private IDisposable _consumer;
        private KafkaSetting _kafkaSetting;

        public KafkaConsumer(KafkaSetting kafkaSetting)
        {
            _kafkaSetting = kafkaSetting;
        }

        public void Subscribe()
        {
             _consumer = Configure
                .Consumer(_kafkaSetting.EventGeographicalAreaUpdatedConsumer, c => c.UseKafka(_kafkaSetting.Server))
                .Serialization(s => s.UseNewtonsoftJson())
                .Topics(t => t.Subscribe(_kafkaSetting.EventGeographicalAreaUpdated))
                .Positions(p => p.StoreInFileSystem(_kafkaSetting.PositionFilePath))
                .Logging(l => l.UseSerilog())
                .Handle(async (messages, context, token) =>
                {
                    foreach (var message in messages)
                    {

                    }
                }).Start();
        }

        public void Dispose()
        {
            _consumer.Dispose();
        }
    }
}
