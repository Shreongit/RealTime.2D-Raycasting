using System.Numerics;

namespace RaycastingVisualizer.Geometry;

public readonly struct Ray2D
{
    public Vector2 Origin { get; }
    public Vector2 Direction { get; }

    public Ray2D(Vector2 origin, Vector2 direction)
    {
        Origin = origin;

        Direction = direction == Vector2.Zero
            ? Vector2.UnitX
            : Vector2.Normalize(direction);
    }

    public Vector2 GetPoint(double distance)
    {
        return Origin + Direction * (float)distance;
    }
}