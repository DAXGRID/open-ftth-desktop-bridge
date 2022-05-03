using MediatR;
using System.Collections.Generic;

namespace OpenFTTH.DesktopBridge.Highlight;

public record Etrs89(float MinX, float MinY, float MaxX, float MaxY);

public class HighlightFeatures : IRequest<Unit>
{
    public string EventType { get; set; }
    public IEnumerable<string> IdentifiedFeatureMrids { get; set; }
    public Etrs89 Etrs89 { get; set; }
    public string FeatureType { get; set; }
    public string Username { get; set; }
}
