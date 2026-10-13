using RaycastingVisualizer.Core;

namespace RaycastingVisualizer;

internal static class Program
{
    private static void Main()
    {
        Console.Title = "Real-Time 2D Raycasting & Field-of-Sight Visualizer";

        var game = new Game();
        game.Run();
    }
}