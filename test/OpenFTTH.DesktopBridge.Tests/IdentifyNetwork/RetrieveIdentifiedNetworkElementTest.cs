using FakeItEasy;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using OpenFTTH.DesktopBridge.Bridge;
using OpenFTTH.DesktopBridge.IdentifyNetwork;

namespace OpenFTTH.DesktopBridge.Tests.IdentifyNetwork
{
    public class RetriveIdentifiedNetworkElementTest
    {
        [Fact]
        public async Task Handle_MulticastsBase64EncodedJsonUpdatedMessage_OnBeingCalled()
        {
            var bridgeServer = A.Fake<IBridgeServer>();
            var retrieveEvent = A.Fake<RetrieveIdentifiedNetworkElement>();

            var handler = new RetrieveIdentifiedNetworkElementhandler(bridgeServer);
            var result = await handler.Handle(retrieveEvent, new CancellationToken());

            A.CallTo(() => bridgeServer.MulticastText(A<string>._)).MustHaveHappenedOnceExactly();
        }
    }
}
