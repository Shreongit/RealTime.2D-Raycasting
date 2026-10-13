using System.Numerics;
using RaycastingVisualizer.Core;
using RaycastingVisualizer.Geometry;

namespace RaycastingVisualizer.World;

public sealed class World
{
    private readonly GameConfig _config;

    public Vector2 PlayerPosition { get; private set; }

    public Vector2 InitialPlayerPosition { get; }

    public List<LineSegment> Walls { get; } = new();

    public World(GameConfig config)
    {
        _config = config;

        InitialPlayerPosition = new Vector2(
            config.WorldWidth / 2f,
            config.WorldHeight / 2f
        );

        PlayerPosition = InitialPlayerPosition;

        BuildDefaultWorld();
    }

    public void MovePlayer(float dx, float dy)
    {
        Vector2 newPosition = PlayerPosition + new Vector2(dx, dy);

        newPosition.X = Math.Clamp(
            newPosition.X,
            1,
            _config.WorldWidth - 2
        );

        newPosition.Y = Math.Clamp(
            newPosition.Y,
            1,
            _config.WorldHeight - 2
        );

        PlayerPosition = newPosition;
    }

    public void ResetPlayer()
    {
        PlayerPosition = InitialPlayerPosition;
    }

    private void BuildDefaultWorld()
    {
        Walls.Clear();

        // Outer boundary.
        Walls.Add(new LineSegment(
            new Vector2(1, 1),
            new Vector2(_config.WorldWidth - 2, 1)
        ));

        Walls.Add(new LineSegment(
            new Vector2(_config.WorldWidth - 2, 1),
            new Vector2(_config.WorldWidth - 2, _config.WorldHeight - 2)
        ));

        Walls.Add(new LineSegment(
            new Vector2(_config.WorldWidth - 2, _config.WorldHeight - 2),
            new Vector2(1, _config.WorldHeight - 2)
        ));

        Walls.Add(new LineSegment(
            new Vector2(1, _config.WorldHeight - 2),
            new Vector2(1, 1)
        ));

        // Interior walls.
        Walls.Add(new LineSegment(
            new Vector2(15, 5),
            new Vector2(30, 5)
        ));

        Walls.Add(new LineSegment(
            new Vector2(30, 5),
            new Vector2(30, 12)
        ));

        Walls.Add(new LineSegment(
            new Vector2(45, 3),
            new Vector2(45, 10)
        ));

        Walls.Add(new LineSegment(
            new Vector2(45, 10),
            new Vector2(60, 10)
        ));

        Walls.Add(new LineSegment(
            new Vector2(12, 17),
            new Vector2(25, 17)
        ));

        Walls.Add(new LineSegment(
            new Vector2(25, 17),
            new Vector2(25, 22)
        ));

        Walls.Add(new LineSegment(
            new Vector2(50, 16),
            new Vector2(68, 16)
        ));

        Walls.Add(new LineSegment(
            new Vector2(50, 16),
            new Vector2(50, 22)
        ));
    }
}