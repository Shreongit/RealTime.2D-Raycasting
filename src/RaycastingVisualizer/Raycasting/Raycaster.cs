using System.Numerics;
using RaycastingVisualizer.Core;
using RaycastingVisualizer.Geometry;
using RaycastingVisualizer.World;

namespace RaycastingVisualizer.Raycasting;

public sealed class Raycaster
{
    private readonly GameConfig _config;

    public List<Ray2D> Rays { get; } = new();
    public List<VisibilityPoint> VisibilityPoints { get; } = new();

    public int IntersectionTests { get; private set; }

    public Raycaster(GameConfig config)
    {
        _config = config;
    }

    public void CastRays(World.World world)
    {
        Rays.Clear();
        VisibilityPoints.Clear();
        IntersectionTests = 0;

        Vector2 origin = world.PlayerPosition;

        double fovRadians =
            _config.FieldOfViewDegrees * Math.PI / 180.0;

        double startAngle = -fovRadians / 2.0;

        for (int i = 0; i < _config.RayCount; i++)
        {
            double t = _config.RayCount == 1
                ? 0.5
                : (double)i / (_config.RayCount - 1);

            double angle = startAngle + fovRadians * t;

            Vector2 direction = new(
                (float)Math.Cos(angle),
                (float)Math.Sin(angle)
            );

            var ray = new Ray2D(origin, direction);

            Rays.Add(ray);

            CastSingleRay(ray, origin, angle, world.Walls);
        }

        VisibilityPoints.Sort(
            (a, b) => a.Angle.CompareTo(b.Angle)
        );
    }

    private void CastSingleRay(
        Ray2D ray,
        Vector2 origin,
        double angle,
        IReadOnlyList<LineSegment> walls)
    {
        IntersectionResult closestHit =
            IntersectionResult.NoHit();

        foreach (LineSegment wall in walls)
        {
            IntersectionTests++;

            IntersectionResult result =
                GeometryMath.RaySegmentIntersection(ray, wall);

            if (!result.Hit)
            {
                continue;
            }

            if (!closestHit.Hit ||
                result.RayDistance < closestHit.RayDistance)
            {
                closestHit = result;
            }
        }

        if (closestHit.Hit)
        {
            VisibilityPoints.Add(
                new VisibilityPoint(
                    closestHit.Point,
                    angle,
                    closestHit.RayDistance
                )
            );
        }
    }
}