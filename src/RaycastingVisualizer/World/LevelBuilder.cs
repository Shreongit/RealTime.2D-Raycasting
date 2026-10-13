using System.Numerics;
using RaycastingVisualizer.Geometry;

namespace RaycastingVisualizer.World;

public static class LevelBuilder
{
    public static List<LineSegment> CreateDefaultLevel(
        int width,
        int height)
    {
        var walls = new List<LineSegment>();

        AddBoundary(walls, width, height);

        AddInteriorWalls(walls);

        return walls;
    }

    private static void AddBoundary(
        List<LineSegment> walls,
        int width,
        int height)
    {
        walls.Add(new LineSegment(
            new Vector2(1, 1),
            new Vector2(width - 2, 1)));

        walls.Add(new LineSegment(
            new Vector2(width - 2, 1),
            new Vector2(width - 2, height - 2)));

        walls.Add(new LineSegment(
            new Vector2(width - 2, height - 2),
            new Vector2(1, height - 2)));

        walls.Add(new LineSegment(
            new Vector2(1, height - 2),
            new Vector2(1, 1)));
    }

    private static void AddInteriorWalls(
        List<LineSegment> walls)
    {
        walls.Add(new LineSegment(
            new Vector2(15, 5),
            new Vector2(30, 5)));

        walls.Add(new LineSegment(
            new Vector2(30, 5),
            new Vector2(30, 12)));

        walls.Add(new LineSegment(
            new Vector2(45, 3),
            new Vector2(45, 10)));

        walls.Add(new LineSegment(
            new Vector2(45, 10),
            new Vector2(60, 10)));

        walls.Add(new LineSegment(
            new Vector2(12, 17),
            new Vector2(25, 17)));

        walls.Add(new LineSegment(
            new Vector2(25, 17),
            new Vector2(25, 22)));

        walls.Add(new LineSegment(
            new Vector2(50, 16),
            new Vector2(68, 16)));

        walls.Add(new LineSegment(
            new Vector2(50, 16),
            new Vector2(50, 22)));
    }
}