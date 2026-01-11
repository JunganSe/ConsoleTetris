using ConsoleTetris.GameComponents;
using ConsoleTetris.Inputs;

namespace ConsoleTetris.Control;

internal class GameManager
{
    private readonly InputManager _inputManager;
    private readonly TetrominoManager _tetrominoManager;

    public Game Game { get; }
    public bool IsTetrominoOnBoard => Game.ActiveTetromino is not null;
    public TetrominoShape NextTetrominoShape { get; private set; }

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
        // TODO: Limit movement speed and soft drop speed.

        if (_inputManager.InputState.IsHeld(Input.Left))
            _tetrominoManager.MoveLeft();

        if (_inputManager.InputState.IsHeld(Input.Right))
            _tetrominoManager.MoveRight();

        if (_inputManager.InputState.IsPressed(Input.SpinLeft))
            _tetrominoManager.SpinLeft();

        if (_inputManager.InputState.IsPressed(Input.SpinRight))
            _tetrominoManager.SpinRight();

        if (_inputManager.InputState.IsHeld(Input.SoftDrop))
            _tetrominoManager.SoftDrop();

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
        if (!IsTetrominoOnBoard)
        {
            _tetrominoManager.SpawnNext();
            NextTetrominoShape = _tetrominoManager.NextShape;
        }

    }
}
