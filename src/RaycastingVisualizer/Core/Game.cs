using RaycastingVisualizer.Rendering;
using RaycastingVisualizer.Raycasting;
using RaycastingVisualizer.World;

namespace RaycastingVisualizer.Core;

public sealed class Game
{
    private readonly GameConfig _config;
    private readonly InputManager _input;
    private readonly World.World _world;
    private readonly Raycaster _raycaster;
    private readonly ConsoleRenderer _renderer;

    private bool _running;

    public Game()
    {
        _config = new GameConfig();
        _input = new InputManager();
        _world = new World.World(_config);
        _raycaster = new Raycaster(_config);
        _renderer = new ConsoleRenderer(_config);

        _running = true;
    }

    public void Run()
    {
        Console.CursorVisible = false;

        while (_running)
        {
            _input.ProcessInput(ref _running, _world);
            _raycaster.CastRays(_world);

            _renderer.Render(
                _world,
                _raycaster
            );

            Thread.Sleep(_config.FrameDelayMilliseconds);
        }

        Console.CursorVisible = true;
        Console.Clear();
    }
}