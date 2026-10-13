using System.Numerics;

namespace RaycastingVisualizer.Utils;

public static class MathUtils
{
    public static double DegreesToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }

    public static double RadiansToDegrees(double radians)
    {
        return radians * 180.0 / Math.PI;
    }

    public static Vector2 FromAngle(double radians)
    {
        return new Vector2(
            (float)Math.Cos(radians),
            (float)Math.Sin(radians)
        );
    }

    public static double NormalizeAngle(double radians)
    {
        double twoPi = Math.PI * 2.0;

        radians %= twoPi;

        if (radians < 0)
        {
            radians += twoPi;
        }

        return radians;
    }

    public static double Distance(
        Vector2 a,
        Vector2 b)
    {
        return Vector2.Distance(a, b);
    }
}