using System.Numerics;

namespace RaycastingVisualizer.Geometry;

public readonly struct LineSegment
{
    public Vector2 Start { get; }
    public Vector2 End { get; }

    public LineSegment(Vector2 start, Vector2 end)
    {
        Start = start;
        End = end;
    }

    public Vector2 Direction => End - Start;

    public double Length => Direction.Length();

    public Vector2 GetPoint(float t)
    {
        return Start + Direction * t;
    }
}