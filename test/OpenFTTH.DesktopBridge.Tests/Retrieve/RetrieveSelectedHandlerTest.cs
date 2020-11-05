using FakeItEasy;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using OpenFTTH.DesktopBridge.Bridge;
using OpenFTTH.DesktopBridge.Retrieve;

namespace OpenFTTH.DesktopBridge.Tests.Retrieve
{
    public class RetrieveSelectedHandlerTest
    {
        [Fact]
        public async Task Handle_MulticastJsonRetrieveSelectedMessage_OnBeingCalled()
        {
            var bridgeServer = A.Fake<IBridgeServer>();
            var retrieveSelected = A.Fake<RetrieveSelected>();

            var retrieveSelectedHandler = new RetrieveSelectedHandler(bridgeServer);
            var result = await retrieveSelectedHandler.Handle(retrieveSelected, new CancellationToken());

            A.CallTo(() => bridgeServer.MulticastText(A<string>._)).MustHaveHappenedOnceExactly();
        }
    }
}
