using System.Numerics;

namespace RaycastingVisualizer.Raycasting;

public sealed class VisibilityPolygon
{
    public IReadOnlyList<Vector2> Points { get; private set; } =
        Array.Empty<Vector2>();

    public void Build(IEnumerable<VisibilityPoint> visibilityPoints)
    {
        Points = visibilityPoints
            .OrderBy(point => point.Angle)
            .Select(point => point.Position)
            .ToList();
    }
}