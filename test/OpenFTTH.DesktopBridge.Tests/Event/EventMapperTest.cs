using Xunit;
using System;
using OpenFTTH.DesktopBridge.IdentifyNetwork;
using OpenFTTH.DesktopBridge.Event;
using OpenFTTH.DesktopBridge.Retrieve;
using FluentAssertions;

namespace OpenFTTH.DesktopBridge.Tests
{
    public class EventMapperTest
    {
        [Theory]
        [InlineData("{ \"eventType\": \"IdentifyNetworkElement\" }", typeof(IdentifyNetworkElement))]
        [InlineData("{ \"eventType\": \"RetrieveSelected\" }", typeof(RetrieveSelected))]
        [InlineData("{ \"eventType\": \"RetrieveSelectedResponse\" }", typeof(RetrieveSelectedResponse))]
        public void Map_ShouldReturnCorrectlyParsedType_OnReceivedJsonEvent(string jsonEvent, Type expectedType)
        {
            var mapper = new EventMapper();

            var result = mapper.Map(jsonEvent);

            result.Should().BeOfType(expectedType);
        }
    }
}
