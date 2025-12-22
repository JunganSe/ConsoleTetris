using ConsoleTetris.GameComponents;
using ConsoleTetris.Inputs;
using ConsoleTetris.Rendering;
using ConsoleTetris.Utilities;
using System.Diagnostics;

namespace ConsoleTetris;

internal class Controller
{
    private const int _targetFps = 30;
    private const double _targetFrameTime = 1000.0 / _targetFps;

    private readonly InputManager _inputManager = new();
    private readonly Playfield _playfield = new();
    private readonly FpsTracker _fpsTracker = new();
    private readonly GuiRenderer _guiRenderer = new();
    private readonly PlayfieldRenderer _playfieldRenderer = new();

    private bool _isRunning = true;

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
        _fpsTracker.Update(deltaTime);

        // TODO: Update game state.
    }

    private void Render()
    {
        // Test code:
        _playfield.Pieces[0, 0] = TetrominoType.I;
        _playfield.Pieces[1, 0] = TetrominoType.O;
        _playfield.Pieces[2, 1] = TetrominoType.T;
        _playfield.Pieces[3, 1] = TetrominoType.S;
        _playfield.Pieces[4, 2] = TetrominoType.Z;
        _playfield.Pieces[5, 2] = TetrominoType.J;
        _playfield.Pieces[6, 2] = TetrominoType.L;
        // Test code end.

        _guiRenderer.DrawFps(_fpsTracker.CurrentFps);
        _playfieldRenderer.DrawPlayfield(_playfield);
        // TODO: Render more game state.
    }
}
