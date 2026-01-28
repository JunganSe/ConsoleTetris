using ConsoleTetris.GameComponents;
using ConsoleTetris.Rendering;
using ConsoleTetris.Utilities;
using System.Diagnostics;

namespace ConsoleTetris.Control;

internal class Controller
{
    private const int _targetFps = 60;
    private const double _targetFrameTime = 1000.0 / _targetFps;

    private readonly GameManager _gameManager = new();
    private readonly FpsTracker _fpsTracker = new();
    private readonly GuiRenderer _guiRenderer = new();
    private readonly PlayfieldRenderer _playfieldRenderer = new();

    private GameState _gameState = GameState.Starting;

    private Playfield Playfield => _gameManager.Game.Playfield;

    public void Start()
    {
        Initialize();
        Run();
    }



    private void Initialize()
    {
        ConsoleManager.InitializeConsole();
        _guiRenderer.DrawGui();
    }

    private void Run()
    {
        _gameState = GameState.Running;
        var stopwatch = Stopwatch.StartNew();
        double lastFrameTime = 0;

        while (_gameState is GameState.Running)
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
        Update(deltaTime);
        Render();
    }

    private void Update(double deltaTime)
    {
        _fpsTracker.Update(deltaTime);
        _gameManager.Update();
        _gameManager.HandleGravity();
        _gameManager.HandleLocking();
        _gameManager.HandleInput();
        _gameManager.HandleCompletedLines();

        if (_gameManager.IsGameLost())
            _gameState = GameState.GameOver;

        _gameManager.SpawnTetrominoIfApplicable();
        _gameManager.UpdateGhost();
    }

    private void Render()
    {
        // TODO: Optimize to only draw next tetromino when it has changed.
        // TODO: Optimize to only draw held tetromino when it has changed.
        // TODO: Draw ghost.
        _playfieldRenderer.DrawPlayfield(Playfield);
        _guiRenderer.DrawLevel(_gameManager.Game.Level);
        _guiRenderer.DrawClearedLines(_gameManager.Game.ClearedLines);
        _guiRenderer.DrawScore(_gameManager.Game.Score);
        _guiRenderer.DrawNextTetromino(_gameManager.NextTetrominoShape);
        _guiRenderer.DrawHeldTetromino(_gameManager.HeldTetrominoShape);
        _guiRenderer.DrawFps(_fpsTracker.CurrentFps);
    }
}
