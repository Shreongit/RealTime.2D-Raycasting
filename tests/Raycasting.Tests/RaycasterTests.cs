using System.Numerics;
using RaycastingVisualizer.Core;
using RaycastingVisualizer.Geometry;
using RaycastingVisualizer.Raycasting;
using RaycastingVisualizer.World;
using Xunit;

namespace Raycasting.Tests;

public class RaycasterTests
{
    [Fact]
    public void CastRays_ShouldGenerateConfiguredNumberOfRays()
    {
        var config = new GameConfig();
        var world = new World(config);
        var raycaster = new Raycaster(config);

        raycaster.CastRays(world);

        Assert.Equal(
            config.RayCount,
            raycaster.Rays.Count);
    }

    [Fact]
    public void CastRays_ShouldDetectWallIntersections()
    {
        var config = new GameConfig();
        var world = new World(config);
        var raycaster = new Raycaster(config);

        raycaster.CastRays(world);

        Assert.NotEmpty(
            raycaster.VisibilityPoints);
    }

    [Fact]
    public void CastRays_ShouldPerformIntersectionTests()
    {
        var config = new GameConfig();
        var world = new World(config);
        var raycaster = new Raycaster(config);

        raycaster.CastRays(world);

        int expectedMinimum =
            config.RayCount * world.Walls.Count;

        Assert.True(
            raycaster.IntersectionTests >= expectedMinimum);
    }

    [Fact]
    public void Raycaster_ShouldFindClosestIntersection()
    {
        var config = new GameConfig();
        var world = new World(config);

        world.Walls.Clear();

        // Player is at the center of the world.
        // Place two horizontal walls inside the
        // configured 120-degree field of view.
        world.Walls.Add(
            new LineSegment(
                new Vector2(20, 10),
                new Vector2(60, 10)));

        world.Walls.Add(
            new LineSegment(
                new Vector2(20, 15),
                new Vector2(60, 15)));

        var raycaster = new Raycaster(config);

        raycaster.CastRays(world);

        Assert.NotEmpty(raycaster.VisibilityPoints);

        var closest = raycaster.VisibilityPoints
            .OrderBy(point => point.Distance)
            .First();

        Assert.True(closest.Distance > 0);
        Assert.True(closest.Distance < 20);
    }
}