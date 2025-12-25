using ConsoleTetris.GameComponents;
using ConsoleTetris.Inputs;
using ConsoleTetris.Rendering;
using ConsoleTetris.Utilities;
using System.Diagnostics;

namespace ConsoleTetris.Control;

internal class Controller
{
    private const int _targetFps = 30;
    private const double _targetFrameTime = 1000.0 / _targetFps;

    private readonly InputManager _inputManager = new();
    private readonly GameManager _gameManager = new();
    private readonly FpsTracker _fpsTracker = new();
    private readonly GuiRenderer _guiRenderer = new();
    private readonly PlayfieldRenderer _playfieldRenderer = new();

    private bool _isRunning = true;

    private Game Game => _gameManager.Game;
    private Playfield Playfield => _gameManager.Game.Playfield;

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
        // Test code:
        Playfield.Pieces[0, 0] = new() { Type = TetrominoType.I, State = TetrominoState.Locked };
        Playfield.Pieces[1, 0] = new() { Type = TetrominoType.O, State = TetrominoState.Locked };
        Playfield.Pieces[2, 1] = new() { Type = TetrominoType.T, State = TetrominoState.Locked };
        Playfield.Pieces[3, 1] = new() { Type = TetrominoType.S, State = TetrominoState.Locked };
        Playfield.Pieces[4, 2] = new() { Type = TetrominoType.Z, State = TetrominoState.Locked };
        Playfield.Pieces[5, 2] = new() { Type = TetrominoType.J, State = TetrominoState.Locked };
        Playfield.Pieces[6, 2] = new() { Type = TetrominoType.L, State = TetrominoState.Locked };
        // Test code end.

        // TODO: Initialize score.

        ConsoleManager.InitializeConsole();
        _guiRenderer.DrawGui();
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
        // TODO: Render more game state.
        _playfieldRenderer.DrawPlayfield(Playfield);
        _guiRenderer.DrawFps(_fpsTracker.CurrentFps);
    }
}
