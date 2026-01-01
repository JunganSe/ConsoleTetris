using ConsoleTetris.GameComponents;
using ConsoleTetris.Inputs;

namespace ConsoleTetris.Control;

internal class GameManager
{
    private readonly InputManager _inputManager;
    private readonly TetrominoManager _tetrominoManager;

    public Game Game { get; }
    public bool IsTetrominoOnBoard => Game.ActiveTetromino is not null;

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

        if (_inputManager.InputState.IsHeld(Input.Left))
            _tetrominoManager.MoveTetrominoLeft();

        if (_inputManager.InputState.IsHeld(Input.Right))
            _tetrominoManager.MoveTetrominoRight();

        if (_inputManager.InputState.IsPressed(Input.SpinLeft))
            _tetrominoManager.RotateTetrominoCounterClockwise();

        if (_inputManager.InputState.IsPressed(Input.SpinRight))
            _tetrominoManager.RotateTetrominoClockwise();

        if (_inputManager.InputState.IsHeld(Input.SoftDrop))
            _tetrominoManager.SoftDropTetromino();

        if (_inputManager.InputState.IsPressed(Input.HardDrop))
        {
            _tetrominoManager.HardDropTetromino();
            _tetrominoManager.LockTetromino();
        }

        if (_inputManager.InputState.IsPressed(Input.Pause)) // Temporary for testing.
            _tetrominoManager.LockTetromino();
    }

    public void SpawnTetrominoIfApplicable()
    {
        if (!IsTetrominoOnBoard)
            _tetrominoManager.SpawnTetromino();

    }
}
