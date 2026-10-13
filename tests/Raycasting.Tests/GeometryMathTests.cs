using System.Numerics;
using RaycastingVisualizer.Geometry;
using Xunit;

namespace Raycasting.Tests;

public class GeometryMathTests
{
    [Fact]
    public void CrossProduct_ShouldCalculateCorrectly()
    {
        Vector2 a = new(1, 0);
        Vector2 b = new(0, 1);

        double result = GeometryMath.Cross(a, b);

        Assert.Equal(1.0, result);
    }

    [Fact]
    public void RaySegmentIntersection_ShouldDetectHit()
    {
        var ray = new Ray2D(
            new Vector2(0, 0),
            new Vector2(1, 0));

        var segment = new LineSegment(
            new Vector2(5, -2),
            new Vector2(5, 2));

        IntersectionResult result =
            GeometryMath.RaySegmentIntersection(ray, segment);

        Assert.True(result.Hit);
        Assert.Equal(5.0, result.RayDistance, 5);
        Assert.Equal(5.0, result.Point.X, 5);
        Assert.Equal(0.0, result.Point.Y, 5);
    }

    [Fact]
    public void RaySegmentIntersection_ShouldRejectParallelLines()
    {
        var ray = new Ray2D(
            new Vector2(0, 0),
            new Vector2(1, 0));

        var segment = new LineSegment(
            new Vector2(0, 5),
            new Vector2(10, 5));

        IntersectionResult result =
            GeometryMath.RaySegmentIntersection(ray, segment);

        Assert.False(result.Hit);
    }

    [Fact]
    public void RaySegmentIntersection_ShouldRejectIntersectionBehindRay()
    {
        var ray = new Ray2D(
            new Vector2(0, 0),
            new Vector2(1, 0));

        var segment = new LineSegment(
            new Vector2(-5, -2),
            new Vector2(-5, 2));

        IntersectionResult result =
            GeometryMath.RaySegmentIntersection(ray, segment);

        Assert.False(result.Hit);
    }

    [Fact]
    public void RaySegmentIntersection_ShouldRejectPointOutsideSegment()
    {
        var ray = new Ray2D(
            new Vector2(0, 5),
            new Vector2(1, 0));

        var segment = new LineSegment(
            new Vector2(5, 0),
            new Vector2(5, 2));

        IntersectionResult result =
            GeometryMath.RaySegmentIntersection(ray, segment);

        Assert.False(result.Hit);
    }

    [Fact]
    public void RaySegmentIntersection_ShouldDetectEndpointHit()
    {
        var ray = new Ray2D(
            new Vector2(0, 0),
            new Vector2(1, 0));

        var segment = new LineSegment(
            new Vector2(5, 0),
            new Vector2(5, 5));

        IntersectionResult result =
            GeometryMath.RaySegmentIntersection(ray, segment);

        Assert.True(result.Hit);
        Assert.Equal(5.0, result.RayDistance, 5);
    }

    [Fact]
    public void Angle_ShouldReturnCorrectAngle()
    {
        Vector2 origin = Vector2.Zero;
        Vector2 point = new(1, 0);

        double angle = GeometryMath.Angle(origin, point);

        Assert.Equal(0.0, angle, 5);
    }
}