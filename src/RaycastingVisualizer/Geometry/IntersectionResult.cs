using System.Numerics;

namespace RaycastingVisualizer.Geometry;

public readonly struct IntersectionResult
{
    public bool Hit { get; }
    public Vector2 Point { get; }
    public double RayDistance { get; }
    public double SegmentPosition { get; }

    private IntersectionResult(
        bool hit,
        Vector2 point,
        double rayDistance,
        double segmentPosition)
    {
        Hit = hit;
        Point = point;
        RayDistance = rayDistance;
        SegmentPosition = segmentPosition;
    }

    public static IntersectionResult NoHit()
    {
        return new IntersectionResult(
            false,
            Vector2.Zero,
            double.PositiveInfinity,
            double.NaN
        );
    }

    public static IntersectionResult Create(
        Vector2 point,
        double rayDistance,
        double segmentPosition)
    {
        return new IntersectionResult(
            true,
            point,
            rayDistance,
            segmentPosition
        );
    }
}