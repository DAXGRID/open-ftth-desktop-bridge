using FakeItEasy;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using OpenFTTH.DesktopBridge.Bridge;
using OpenFTTH.DesktopBridge.Pan;

namespace OpenFTTH.DesktopBridge.Tests.Pan
{
    public class PanToCoordinateHandlerTest
    {
        [Fact]
        public async Task Handle_MulticastsJsonUpdatedMessage_OnBeingCalled()
        {
            var bridgeServer = A.Fake<IBridgeServer>();
            var identifyNetworkElement = A.Fake<PanToCoordinate>();

            var panToCoordinateHandler = new PanToCoordinateHandler(bridgeServer);
            var result = await panToCoordinateHandler.Handle(identifyNetworkElement, new CancellationToken());

            A.CallTo(() => bridgeServer.MulticastText(A<string>._)).MustHaveHappenedOnceExactly();
        }
    }
}
