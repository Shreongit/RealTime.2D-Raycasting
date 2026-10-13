using System.Numerics;
using RaycastingVisualizer.Geometry;

namespace RaycastingVisualizer.World;

public sealed class Wall
{
    public LineSegment Segment { get; }

    public Vector2 Start => Segment.Start;
    public Vector2 End => Segment.End;

    public Wall(Vector2 start, Vector2 end)
    {
        Segment = new LineSegment(start, end);
    }
}