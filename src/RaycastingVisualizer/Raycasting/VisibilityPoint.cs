using System.Numerics;

namespace RaycastingVisualizer.Raycasting;

public readonly struct VisibilityPoint
{
    public Vector2 Position { get; }
    public double Angle { get; }
    public double Distance { get; }

    public VisibilityPoint(
        Vector2 position,
        double angle,
        double distance)
    {
        Position = position;
        Angle = angle;
        Distance = distance;
    }
}