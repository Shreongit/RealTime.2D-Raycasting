namespace RaycastingVisualizer.Core;

public sealed class GameConfig
{
    public int ConsoleWidth { get; } = 100;
    public int ConsoleHeight { get; } = 35;

    public int WorldWidth { get; } = 80;
    public int WorldHeight { get; } = 25;

    public int RayCount { get; } = 180;

    public double FieldOfViewDegrees { get; } = 120.0;

    public int FrameDelayMilliseconds { get; } = 50;

    public char EmptyCharacter { get; } = ' ';
    public char WallCharacter { get; } = '#';
    public char PlayerCharacter { get; } = '@';
    public char RayCharacter { get; } = '.';
    public char IntersectionCharacter { get; } = 'o';
    public char VisibilityCharacter { get; } = '+';
}