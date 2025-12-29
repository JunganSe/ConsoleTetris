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
        _inputManager.Update();

        Update(deltaTime);
        Render();
    }

    private void Update(double deltaTime)
    {
        _fpsTracker.Update(deltaTime);

        if (!_gameManager.IsTetrominoOnBoard)
            _gameManager.SpawnTetromino();

        if (_inputManager.InputState.IsPressed(Input.SpinRight))
            _gameManager.RotateTetrominoClockwise();

        if (_inputManager.InputState.IsPressed(Input.SpinLeft))
            _gameManager.RotateTetrominoCounterClockwise();

        if (_inputManager.InputState.IsHeld(Input.SoftDrop))
            _gameManager.MoveTetrominoDown();

        if (_inputManager.InputState.IsHeld(Input.Left))
            _gameManager.MoveTetrominoLeft();

        if (_inputManager.InputState.IsHeld(Input.Right))
            _gameManager.MoveTetrominoRight();

        // TODO: Update more game state.
    }

    private void Render()
    {
        // TODO: Render more game state.
        _playfieldRenderer.DrawPlayfield(Playfield);
        _guiRenderer.DrawFps(_fpsTracker.CurrentFps);
    }
}
