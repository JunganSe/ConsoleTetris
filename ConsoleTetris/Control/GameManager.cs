using ConsoleTetris.GameComponents;
using ConsoleTetris.Inputs;

namespace ConsoleTetris.Control;

internal class GameManager
{
    private readonly InputManager _inputManager;
    private readonly TetrominoManager _tetrominoManager;

    private int _framesSinceMoveLeft = 0;
    private int _framesSinceMoveRight = 0;
    private int _framesSinceMoveDown = 0;

    public Game Game { get; }
    public TetrominoShape NextTetrominoShape => _tetrominoManager.NextShape;
    public TetrominoShape? HeldTetrominoShape => _tetrominoManager.HeldShape;

    public GameManager()
    {
        _inputManager = new();
        Game = new();
        _tetrominoManager = new(Game);
    }

    public void UpdateInput()
    {
        _inputManager.Update();
    }

    public void HandleInput()
    {
        _framesSinceMoveLeft++;
        _framesSinceMoveRight++;
        _framesSinceMoveDown++;

        if (_inputManager.InputState.IsHeld(Input.Left) && _framesSinceMoveLeft >= 3)
        {
            _framesSinceMoveLeft = 0;
            _tetrominoManager.MoveLeft();
        }

        if (_inputManager.InputState.IsHeld(Input.Right) && _framesSinceMoveRight >= 3)
        {
            _framesSinceMoveRight = 0;
            _tetrominoManager.MoveRight();
        }

        if (_inputManager.InputState.IsPressed(Input.SpinLeft))
            _tetrominoManager.SpinLeft();

        if (_inputManager.InputState.IsPressed(Input.SpinRight))
            _tetrominoManager.SpinRight();

        if (_inputManager.InputState.IsHeld(Input.SoftDrop) && _framesSinceMoveDown >= 2)
        {
            _framesSinceMoveDown = 0;
            _tetrominoManager.SoftDrop();
        }

        if (_inputManager.InputState.IsPressed(Input.HardDrop))
        {
            _tetrominoManager.HardDrop();
            _tetrominoManager.Lock();
        }

        if (_inputManager.InputState.IsPressed(Input.Hold))
            _tetrominoManager.Hold();

        if (_inputManager.InputState.IsPressed(Input.Pause))
            _tetrominoManager.Lock(); // Temporary for testing.
    }

    public void SpawnTetrominoIfApplicable()
    {
        if (Game.ActiveTetromino is null)
            _tetrominoManager.SpawnNext();
    }
}
