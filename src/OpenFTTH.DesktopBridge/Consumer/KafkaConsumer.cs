using System;
using Topos.Config;
using Topos.Serialization;

namespace OpenFTTH.DesktopBridge.Consumer
{
    public class KafkaConsumer : IConsumer, IDisposable
    {
        private IDisposable _consumer;

        public void Subscribe()
        {
             // _consumer = Configure
             //    .Consumer("DesktopBridgeConsumer", c => c.UseKafka(_kafkaSetting.Server))
             //    .Serialization(s => s.RouteNetwork())
             //    .Topics(t => t.Subscribe(_kafkaSetting.PostgisRouteNetworkTopic))
             //    .Positions(p => p.StoreInFileSystem(_kafkaSetting.PositionFilePath))
             //    .Logging(l => l.UseSerilog())
             //    .Handle(async (messages, context, token) =>
             //    {
             //        foreach (var message in messages)
             //        {

             //        }
             //    }).Start();
        }

        public void Dispose()
        {
            _consumer.Dispose();
        }
    }
}
