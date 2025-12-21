using ConsoleTetris.Inputs;
using ConsoleTetris.Rendering;
using System.Diagnostics;

namespace ConsoleTetris;

internal class Game
{
    private const int _targetFps = 30;
    private const double _targetFrameTime = 1000.0 / _targetFps;

    private readonly InputManager _inputManager = new();
    private readonly GuiRenderer _guiRenderer = new();
    private bool _isRunning = true;

    private double _currentFps = 0;
    private double _fpsUpdateTimer = 0;
    private const double _fpsUpdateInterval = 200; // Update FPS display every 200ms

    public void Start()
    {
        Initialize();
        Run();
    }

    public void Stop()
    {
        _isRunning = false;
    }



    private void Initialize()
    {
        // TODO: Initialize game.
        // - Set up game board.
        // - Initialize score.
        // - Set up input handling.
        // - Set up rendering.

        ConsoleManager.InitializeConsole();
        _guiRenderer.DrawPlayAreaBorder();
    }

    private void Run()
    {
        var stopwatch = Stopwatch.StartNew();
        double lastFrameTime = 0;

        while (_isRunning)
        {
            double currentTime = stopwatch.Elapsed.TotalMilliseconds;
            double deltaTime = currentTime - lastFrameTime;
            lastFrameTime = currentTime;

            MainLoop(deltaTime);

            double frameTime = stopwatch.Elapsed.TotalMilliseconds - currentTime;
            double sleepTime = _targetFrameTime - frameTime;
            if (sleepTime > 0)
                Thread.Sleep((int)sleepTime);
        }
    }

    private void MainLoop(double deltaTime)
    {
        _inputManager.Update();

        Update(deltaTime);
        Render();
    }

    private void Update(double deltaTime)
    {
        _fpsUpdateTimer += deltaTime;
        if (_fpsUpdateTimer >= _fpsUpdateInterval)
        {
            _currentFps = deltaTime > 0 ? 1000.0 / deltaTime : 0;
            _fpsUpdateTimer = 0;
        }

        // TODO: Update game state.
    }

    private void Render()
    {
        _guiRenderer.DrawFps(_currentFps);
        // TODO: Render game state.
    }
}
