using FakeItEasy;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using OpenFTTH.DesktopBridge.Bridge;
using OpenFTTH.DesktopBridge.Retrieve;

namespace OpenFTTH.DesktopBridge.Tests.Retrieve
{
    public class RetrieveSelectedResponseHandlerTest
    {
        [Fact]
        public async Task Handle_MulticastsJsonRetrieveSelectedResponse_OnBeingCalled()
        {
            var bridgeServer = A.Fake<IBridgeServer>();
            var retrieveSelectedResponse = A.Fake<RetrieveSelectedResponse>();

            var retrieveSelectedHandler = new RetrieveSelectedResponseHandler(bridgeServer);
            var result = await retrieveSelectedHandler.Handle(retrieveSelectedResponse, new CancellationToken());

            A.CallTo(() => bridgeServer.MulticastText(A<string>._)).MustHaveHappenedOnceExactly();
        }
    }
}
