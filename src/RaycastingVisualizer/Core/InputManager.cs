using RaycastingVisualizer.World;

namespace RaycastingVisualizer.Core;

public sealed class InputManager
{
    public void ProcessInput(ref bool running, World.World world)
    {
        while (Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey(intercept: true).Key;

            switch (key)
            {
                case ConsoleKey.W:
                    world.MovePlayer(0, -1);
                    break;

                case ConsoleKey.S:
                    world.MovePlayer(0, 1);
                    break;

                case ConsoleKey.A:
                    world.MovePlayer(-1, 0);
                    break;

                case ConsoleKey.D:
                    world.MovePlayer(1, 0);
                    break;

                case ConsoleKey.R:
                    world.ResetPlayer();
                    break;

                case ConsoleKey.Escape:
                    running = false;
                    break;
            }
        }
    }
}