using System.Collections.Generic;
using MediatR;

namespace OpenFTTH.DesktopBridge.Highlight;

public class HighlightFeatures : IRequest<Unit>
{
    public string EventType { get; set; }
    public IEnumerable<string> IdentifiedFeatureMrids { get; set; }
    public string FeatureType { get; set; }
    public string Username { get; set; }
}
