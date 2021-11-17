using MediatR;
using System.Collections.Generic;

namespace OpenFTTH.DesktopBridge.Retrieve;

public class RetrieveSelectedResponse : IRequest
{
    public string EventType { get; set; }
    public string Username { get; set; }
    public List<string> SelectedFeaturesMrid { get; set; }
}
