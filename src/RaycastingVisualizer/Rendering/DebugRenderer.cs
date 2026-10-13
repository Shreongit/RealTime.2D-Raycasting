using System.Numerics;
using RaycastingVisualizer.Core;
using RaycastingVisualizer.Raycasting;
using RaycastingVisualizer.World;

namespace RaycastingVisualizer.Rendering;

public sealed class DebugRenderer
{
    private readonly GameConfig _config;

    public DebugRenderer(GameConfig config)
    {
        _config = config;
    }

    public void RenderDebugInfo(
        World.World world,
        Raycaster raycaster)
    {
        Console.WriteLine();
        Console.WriteLine("DEBUG INFORMATION");
        Console.WriteLine("------------------");

        Console.WriteLine(
            $"Player Position : " +
            $"({world.PlayerPosition.X:F2}, " +
            $"{world.PlayerPosition.Y:F2})");

        Console.WriteLine(
            $"Ray Count       : {raycaster.Rays.Count}");

        Console.WriteLine(
            $"Wall Count      : {world.Walls.Count}");

        Console.WriteLine(
            $"Visible Points  : " +
            $"{raycaster.VisibilityPoints.Count}");

        Console.WriteLine(
            $"Intersection Tests: " +
            $"{raycaster.IntersectionTests}");

        if (raycaster.VisibilityPoints.Count > 0)
        {
            double nearest = raycaster.VisibilityPoints
                .Min(point => point.Distance);

            double farthest = raycaster.VisibilityPoints
                .Max(point => point.Distance);

            Console.WriteLine(
                $"Nearest Hit     : {nearest:F2}");

            Console.WriteLine(
                $"Farthest Hit    : {farthest:F2}");
        }

        Console.WriteLine();

        RenderWallData(world);
    }

    private void RenderWallData(World.World world)
    {
        Console.WriteLine("WALL SEGMENTS");
        Console.WriteLine("-------------");

        for (int i = 0; i < world.Walls.Count; i++)
        {
            var wall = world.Walls[i];

            Console.WriteLine(
                $"Wall {i + 1:D2}: " +
                $"({wall.Start.X:F1}, {wall.Start.Y:F1}) -> " +
                $"({wall.End.X:F1}, {wall.End.Y:F1})");
        }
    }
}