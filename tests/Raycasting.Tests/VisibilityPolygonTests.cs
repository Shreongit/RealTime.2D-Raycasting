using System.Numerics;
using RaycastingVisualizer.Raycasting;
using Xunit;

namespace Raycasting.Tests;

public class VisibilityPolygonTests
{
    [Fact]
    public void Build_ShouldSortPointsByAngle()
    {
        var visibilityPoints = new List<VisibilityPoint>
        {
            new(
                new Vector2(0, 1),
                Math.PI / 2,
                1),

            new(
                new Vector2(1, 0),
                0,
                1),

            new(
                new Vector2(-1, 0),
                Math.PI,
                1)
        };

        var polygon = new VisibilityPolygon();

        polygon.Build(visibilityPoints);

        Assert.Equal(3, polygon.Points.Count);

        Assert.Equal(
            new Vector2(1, 0),
            polygon.Points[0]);

        Assert.Equal(
            new Vector2(0, 1),
            polygon.Points[1]);

        Assert.Equal(
            new Vector2(-1, 0),
            polygon.Points[2]);
    }

    [Fact]
    public void Build_WithEmptyInput_ShouldProduceEmptyPolygon()
    {
        var polygon = new VisibilityPolygon();

        polygon.Build(Array.Empty<VisibilityPoint>());

        Assert.Empty(polygon.Points);
    }

    [Fact]
    public void Build_ShouldPreserveAllVisibilityPoints()
    {
        var visibilityPoints = new List<VisibilityPoint>
        {
            new(new Vector2(1, 0), 0, 1),
            new(new Vector2(0, 1), 1, 1),
            new(new Vector2(-1, 0), 2, 1),
            new(new Vector2(0, -1), 3, 1)
        };

        var polygon = new VisibilityPolygon();

        polygon.Build(visibilityPoints);

        Assert.Equal(
            visibilityPoints.Count,
            polygon.Points.Count);
    }
}