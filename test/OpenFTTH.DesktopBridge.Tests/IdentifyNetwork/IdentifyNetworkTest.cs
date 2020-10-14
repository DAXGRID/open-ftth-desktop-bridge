using FakeItEasy;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using OpenFTTH.DesktopBridge.Bridge;
using OpenFTTH.DesktopBridge.IdentifyNetwork;

namespace OpenFTTH.DesktopBridge.Tests.IdentifyNetwork
{
    public class IdentifyNetworkHandlerTest
    {
        [Fact]
        public async Task Handle_MulticastsBase64EncodedJsonUpdatedMessage_OnBeingCalled()
        {
            var bridgeServer = A.Fake<IBridgeServer>();
            var identifyNetworkElement = A.Fake<IdentifyNetworkElement>();

            var identifyNetworkHandler = new IdentifyNetworkElementHandler(bridgeServer);
            var result = await identifyNetworkHandler.Handle(identifyNetworkElement, new CancellationToken());

            A.CallTo(() => bridgeServer.MulticastText(A<string>._)).MustHaveHappenedOnceExactly();
        }
    }
}
