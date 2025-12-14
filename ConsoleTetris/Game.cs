using ConsoleTetris.Inputs;
using ConsoleTetris.Rendering;
using System.Diagnostics;

namespace ConsoleTetris;

internal class Game
{
    private const int _targetFps = 60;
    private const double _targetFrameTime = 1000.0 / _targetFps;
    private bool _isRunning = true;
    private Renderer _renderer = new Renderer();

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
        
        _renderer.DrawFrame(0, 0, 20, 20, doubleWall: true); // TODO: Find and use correct dimensions.
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
        var input = GetInput();
        Update(deltaTime);
        Render();
    }

    private Input GetInput()
    {
        return InputReader_Single.ReadInput();
    }

    private void Update(double deltaTime)
    {
        // TODO: Update game state.
    }

    private void Render()
    {
        // TODO: Render game state.
    }
}
