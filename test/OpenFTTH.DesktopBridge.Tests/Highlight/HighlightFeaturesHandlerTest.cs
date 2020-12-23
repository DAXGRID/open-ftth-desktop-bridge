using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using OpenFTTH.DesktopBridge.Bridge;
using Xunit;

namespace OpenFTTH.DesktopBridge.Highlight.Tests
{
    public class HighlightFeaturesHandlerTests
    {
        [Fact]
        public async Task Handle_MulticastsJsonUpdatedMessage_OnBeingCalled()
        {
            var bridgeServer = A.Fake<IBridgeServer>();
            var highlightFeatures = A.Fake<HighlightFeatures>();

            var highlightFeatureHandler = new HighlightFeaturesHandler(bridgeServer);
            var result = await highlightFeatureHandler.Handle(highlightFeatures, new CancellationToken());

            A.CallTo(() => bridgeServer.MulticastText(A<string>._)).MustHaveHappenedOnceExactly();
        }
    }
}
