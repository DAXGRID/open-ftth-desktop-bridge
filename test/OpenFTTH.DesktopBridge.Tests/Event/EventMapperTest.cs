using Xunit;
using System;
using OpenFTTH.DesktopBridge.IdentifyNetwork;
using OpenFTTH.DesktopBridge.Event;
using OpenFTTH.DesktopBridge.Retrieve;
using OpenFTTH.DesktopBridge.Highlight;
using OpenFTTH.DesktopBridge.Pan;
using FluentAssertions;
using OpenFTTH.DesktopBridge.UserError;

namespace OpenFTTH.DesktopBridge.Tests
{
    public class EventMapperTest
    {
        [Theory]
        [InlineData("{ \"eventType\": \"IdentifyNetworkElement\" }", typeof(IdentifyNetworkElement))]
        [InlineData("{ \"eventType\": \"RetrieveIdentifiedNetworkElement\" }", typeof(RetrieveIdentifiedNetworkElement))]
        [InlineData("{ \"eventType\": \"RetrieveSelected\" }", typeof(RetrieveSelected))]
        [InlineData("{ \"eventType\": \"RetrieveSelectedResponse\" }", typeof(RetrieveSelectedResponse))]
        [InlineData("{ \"eventType\": \"PanToCoordinate\" }", typeof(PanToCoordinate))]
        [InlineData("{ \"eventType\": \"HighlightFeatures\" }", typeof(HighlightFeatures))]
        [InlineData("{ \"eventType\": \"SelectRouteSegments\" }", typeof(SelectRouteSegments))]
        public void Map_ShouldReturnCorrectlyParsedType_OnReceivedJsonEvent(string jsonEvent, Type expectedType)
        {
            var mapper = new EventMapper();

            var result = mapper.Map(jsonEvent);

            result.Should().BeOfType(expectedType);
        }
    }
}
