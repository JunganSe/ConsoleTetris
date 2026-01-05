using ConsoleTetris.GameComponents;
using ConsoleTetris.Rendering;
using ConsoleTetris.Utilities;
using System.Diagnostics;

namespace ConsoleTetris.Control;

internal class Controller
{
    private const int _targetFps = 30;
    private const double _targetFrameTime = 1000.0 / _targetFps;

    private readonly GameManager _gameManager = new();
    private readonly FpsTracker _fpsTracker = new();
    private readonly GuiRenderer _guiRenderer = new();
    private readonly PlayfieldRenderer _playfieldRenderer = new();

    private bool _isRunning = true;

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
        Playfield.Pieces[0, 0] = new() { Shape = TetrominoShape.I, State = TetrominoState.Locked };
        Playfield.Pieces[1, 0] = new() { Shape = TetrominoShape.O, State = TetrominoState.Locked };
        Playfield.Pieces[2, 1] = new() { Shape = TetrominoShape.T, State = TetrominoState.Locked };
        Playfield.Pieces[3, 1] = new() { Shape = TetrominoShape.S, State = TetrominoState.Locked };
        Playfield.Pieces[4, 2] = new() { Shape = TetrominoShape.Z, State = TetrominoState.Locked };
        Playfield.Pieces[5, 2] = new() { Shape = TetrominoShape.J, State = TetrominoState.Locked };
        Playfield.Pieces[6, 2] = new() { Shape = TetrominoShape.L, State = TetrominoState.Locked };
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
            double frameStartTime = stopwatch.Elapsed.TotalMilliseconds;
            double deltaTime = frameStartTime - lastFrameTime;
            lastFrameTime = frameStartTime;

            MainLoop(deltaTime);

            double frameTime = stopwatch.Elapsed.TotalMilliseconds - frameStartTime;
            double sleepTime = _targetFrameTime - frameTime;
            double targetEndTime = frameStartTime + _targetFrameTime;

            // Spin waiting for up to 23ms is very cpu heavy, but the sleep oversteps if the value is lower.
            if (sleepTime > 23)
                Thread.Sleep((int)(sleepTime - 1));

            while (stopwatch.Elapsed.TotalMilliseconds < targetEndTime)
            {
                Thread.SpinWait(100);
            }
        }
    }

    private void MainLoop(double deltaTime)
    {
        _gameManager.UpdateInput();
        Update(deltaTime);
        Render();
    }

    private void Update(double deltaTime)
    {
        _fpsTracker.Update(deltaTime);

        _gameManager.HandleInput();
        _gameManager.SpawnTetrominoIfApplicable();

        // TODO: Move tetromino down based on timer.
        // TODO: Lock tetromino if it cannot move down or overlaps a piece.
        // TODO: Update score.
        // TODO: Update more things?
    }

    private void Render()
    {
        // TODO: Render Score.
        // TODO: Render Held tetromino.
        _playfieldRenderer.DrawPlayfield(Playfield);
        _guiRenderer.DrawNextTetromino(_gameManager.NextTetrominoShape);
        _guiRenderer.DrawFps(_fpsTracker.CurrentFps);
    }
}
