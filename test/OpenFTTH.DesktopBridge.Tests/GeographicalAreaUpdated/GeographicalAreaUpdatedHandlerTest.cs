using FakeItEasy;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using OpenFTTH.DesktopBridge.GeographicalAreaUpdated;
using OpenFTTH.DesktopBridge.Bridge;
using FluentAssertions;

namespace OpenFTTH.DesktopBridge.Tests
{
    public class GeographicalAreaUpdatedHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_OnObjectsWithinGeographicalAreaUpdatedBeingNull()
        {
            var bridgeServer = A.Fake<IBridgeServer>();
            var geographicalAreaUpdated = A.Fake<GeographicalAreaUpdated.GeographicalAreaUpdated>();

            A.CallTo(() => geographicalAreaUpdated.ObjectsWithinGeographicalAreaUpdated).Returns(null);

            var geographicalAreaUpdatedHandler = new GeographicalAreaUpdatedHandler(bridgeServer);

            Func<Task> act = async () => await geographicalAreaUpdatedHandler.Handle(geographicalAreaUpdated, new CancellationToken ());
            await act.Should().ThrowExactlyAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task Handle_MulticastsBase64EncodedJsonUpdatedMessage_OnBeingCalled()
        {
            var bridgeServer = A.Fake<IBridgeServer>();
            var geographicalAreaUpdated = A.Fake<GeographicalAreaUpdated.GeographicalAreaUpdated>();

            var geographicalAreaUpdatedHandler = new GeographicalAreaUpdatedHandler(bridgeServer);
            var result = await geographicalAreaUpdatedHandler.Handle(geographicalAreaUpdated, new CancellationToken());

            A.CallTo(() => bridgeServer.MulticastText(A<string>._)).MustHaveHappenedOnceExactly();
        }
    }
}
