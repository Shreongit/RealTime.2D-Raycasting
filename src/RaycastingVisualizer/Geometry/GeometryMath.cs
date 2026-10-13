using System.Numerics;

namespace RaycastingVisualizer.Geometry;

public static class GeometryMath
{
    private const double Epsilon = 1e-9;

    public static double Cross(Vector2 a, Vector2 b)
    {
        return (double)a.X * b.Y - (double)a.Y * b.X;
    }

    public static IntersectionResult RaySegmentIntersection(
        Ray2D ray,
        LineSegment segment)
    {
        Vector2 p = ray.Origin;
        Vector2 r = ray.Direction;

        Vector2 q = segment.Start;
        Vector2 s = segment.Direction;

        double denominator = Cross(r, s);

        // Ray and segment are parallel.
        if (Math.Abs(denominator) < Epsilon)
        {
            return IntersectionResult.NoHit();
        }

        Vector2 qMinusP = q - p;

        double t = Cross(qMinusP, s) / denominator;
        double u = Cross(qMinusP, r) / denominator;

        // Intersection must be in front of the ray.
        if (t < 0)
        {
            return IntersectionResult.NoHit();
        }

        // Intersection must lie inside the line segment.
        if (u < 0 || u > 1)
        {
            return IntersectionResult.NoHit();
        }

        Vector2 intersectionPoint = ray.GetPoint(t);

        return IntersectionResult.Create(
            intersectionPoint,
            t,
            u
        );
    }

    public static double Distance(Vector2 a, Vector2 b)
    {
        return Vector2.Distance(a, b);
    }

    public static double Angle(Vector2 origin, Vector2 point)
    {
        Vector2 direction = point - origin;

        return Math.Atan2(direction.Y, direction.X);
    }
}