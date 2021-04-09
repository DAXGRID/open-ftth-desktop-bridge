using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using OpenFTTH.DesktopBridge.Bridge;
using Xunit;

namespace OpenFTTH.DesktopBridge.Highlight.Tests
{
    public class SelectRouteSegmentsHandlerTest
    {
        [Fact]
        public async Task Handle_MulticastsJsonUpdatedMessage_OnBeingCalled()
        {
            var bridgeServer = A.Fake<IBridgeServer>();
            var selectRouteSegments = A.Fake<SelectRouteSegments>();

            var selectRouteSegmentsHandler = new SelectRouteSegmentsHandler(bridgeServer);
            var result = await selectRouteSegmentsHandler.Handle(selectRouteSegments, new CancellationToken());

            A.CallTo(() => bridgeServer.MulticastText(A<string>._)).MustHaveHappenedOnceExactly();
        }
    }
}
