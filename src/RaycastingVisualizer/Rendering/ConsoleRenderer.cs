using System.Numerics;
using RaycastingVisualizer.Core;
using RaycastingVisualizer.Geometry;
using RaycastingVisualizer.Raycasting;
using RaycastingVisualizer.World;
using System.Text;

namespace RaycastingVisualizer.Rendering;

public sealed class ConsoleRenderer
{
    private readonly GameConfig _config;

    public ConsoleRenderer(GameConfig config)
    {
        _config = config;
    }

    public void Render(
        World.World world,
        Raycaster raycaster)
    {
        Console.SetCursorPosition(0, 0);

        char[,] buffer = CreateBuffer();

        DrawVisibility(buffer, world, raycaster);
        DrawRays(buffer, world, raycaster);
        DrawWalls(buffer, world);
        DrawIntersections(buffer, raycaster);
        DrawPlayer(buffer, world);

        PrintBuffer(buffer);
        PrintStatistics(world, raycaster);
    }

    private char[,] CreateBuffer()
    {
        var buffer = new char[
            _config.WorldHeight,
            _config.WorldWidth];

        for (int y = 0; y < _config.WorldHeight; y++)
        {
            for (int x = 0; x < _config.WorldWidth; x++)
            {
                buffer[y, x] = _config.EmptyCharacter;
            }
        }

        return buffer;
    }

    private void DrawWalls(
        char[,] buffer,
        World.World world)
    {
        foreach (var wall in world.Walls)
        {
            DrawLine(
                buffer,
                wall.Start,
                wall.End,
                _config.WallCharacter);
        }
    }

    private void DrawRays(
        char[,] buffer,
        World.World world,
        Raycaster raycaster)
    {
        foreach (Ray2D ray in raycaster.Rays)
        {
            Vector2 end = ray.GetPoint(
                Math.Max(
                    _config.WorldWidth,
                    _config.WorldHeight));

            DrawLine(
                buffer,
                ray.Origin,
                end,
                _config.RayCharacter);
        }
    }

    private void DrawVisibility(
        char[,] buffer,
        World.World world,
        Raycaster raycaster)
    {
        if (raycaster.VisibilityPoints.Count < 3)
        {
            return;
        }

        for (int i = 0; i < raycaster.VisibilityPoints.Count; i++)
        {
            Vector2 start =
                raycaster.VisibilityPoints[i].Position;

            Vector2 end =
                raycaster.VisibilityPoints[
                    (i + 1) % raycaster.VisibilityPoints.Count
                ].Position;

            DrawLine(
                buffer,
                start,
                end,
                _config.VisibilityCharacter);
        }
    }

    private void DrawIntersections(
        char[,] buffer,
        Raycaster raycaster)
    {
        foreach (var point in raycaster.VisibilityPoints)
        {
            SetPixel(
                buffer,
                point.Position,
                _config.IntersectionCharacter);
        }
    }

    private void DrawPlayer(
        char[,] buffer,
        World.World world)
    {
        SetPixel(
            buffer,
            world.PlayerPosition,
            _config.PlayerCharacter);
    }

    private void DrawLine(
        char[,] buffer,
        Vector2 start,
        Vector2 end,
        char character)
    {
        int x0 = (int)Math.Round(start.X);
        int y0 = (int)Math.Round(start.Y);

        int x1 = (int)Math.Round(end.X);
        int y1 = (int)Math.Round(end.Y);

        int dx = Math.Abs(x1 - x0);
        int dy = Math.Abs(y1 - y0);

        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;

        int error = dx - dy;

        while (true)
        {
            SetPixel(
                buffer,
                x0,
                y0,
                character);

            if (x0 == x1 && y0 == y1)
            {
                break;
            }

            int doubleError = 2 * error;

            if (doubleError > -dy)
            {
                error -= dy;
                x0 += sx;
            }

            if (doubleError < dx)
            {
                error += dx;
                y0 += sy;
            }
        }
    }

    private void SetPixel(
        char[,] buffer,
        Vector2 position,
        char character)
    {
        SetPixel(
            buffer,
            (int)Math.Round(position.X),
            (int)Math.Round(position.Y),
            character);
    }

    private void SetPixel(
        char[,] buffer,
        int x,
        int y,
        char character)
    {
        if (x < 0 ||
            x >= _config.WorldWidth ||
            y < 0 ||
            y >= _config.WorldHeight)
        {
            return;
        }

        buffer[y, x] = character;
    }

    private void PrintBuffer(char[,] buffer)
    {
        var frame = new StringBuilder();

        for (int y = 0; y < _config.WorldHeight; y++)
        {
            for (int x = 0; x < _config.WorldWidth; x++)
            {
                frame.Append(buffer[y, x]);
            }

            frame.AppendLine();
        }

        Console.Write(frame);
    }
    
    private void PrintStatistics(
        World.World world,
        Raycaster raycaster)
    {
        Console.WriteLine();
        Console.WriteLine(
            "============================================================");

        Console.WriteLine(
            "        REAL-TIME 2D RAYCASTING VISUALIZER");

        Console.WriteLine(
            "============================================================");

        Console.WriteLine(
            $"Player: ({world.PlayerPosition.X:F1}, " +
            $"{world.PlayerPosition.Y:F1})");

        Console.WriteLine(
            $"Rays: {raycaster.Rays.Count}");

        Console.WriteLine(
            $"Walls: {world.Walls.Count}");

        Console.WriteLine(
            $"Intersections: {raycaster.VisibilityPoints.Count}");

        Console.WriteLine(
            $"Intersection Tests: {raycaster.IntersectionTests}");

        Console.WriteLine();

        Console.WriteLine(
            "[WASD] Move   [R] Reset   [ESC] Exit");
    }
}