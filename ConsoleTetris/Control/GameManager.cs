using ConsoleTetris.GameComponents;
using ConsoleTetris.Inputs;

namespace ConsoleTetris.Control;

internal class GameManager
{
    private readonly TetrominoManager _tetrominoManager;
    private readonly InputManager _inputManager;
    private readonly InputCooldown _inputCooldown;

    public Game Game { get; }
    public TetrominoShape NextTetrominoShape => _tetrominoManager.NextShape;
    public TetrominoShape? HeldTetrominoShape => _tetrominoManager.HeldShape;

    public GameManager()
    {
        Game = new();
        _tetrominoManager = new(Game);
        _inputManager = new();
        _inputCooldown = new();
        _inputCooldown.SetCooldown(Input.Left, 3);
        _inputCooldown.SetCooldown(Input.Right, 3);
        _inputCooldown.SetCooldown(Input.SoftDrop, 2);
    }

    public void UpdateInput()
    {
        _inputManager.Update();
    }

    public void HandleInput()
    {
        // TODO: Buffer inputs if cooldown is not ready.

        _inputCooldown.Update();

        if (_inputManager.InputState.IsHeld(Input.Left) && _inputCooldown.IsReady(Input.Left))
        {
            _inputCooldown.Reset(Input.Left);
            _tetrominoManager.MoveLeft();
        }

        if (_inputManager.InputState.IsHeld(Input.Right) && _inputCooldown.IsReady(Input.Right))
        {
            _inputCooldown.Reset(Input.Right);
            _tetrominoManager.MoveRight();
        }

        if (_inputManager.InputState.IsPressed(Input.SpinLeft))
            _tetrominoManager.SpinLeft();

        if (_inputManager.InputState.IsPressed(Input.SpinRight))
            _tetrominoManager.SpinRight();

        if (_inputManager.InputState.IsHeld(Input.SoftDrop) && _inputCooldown.IsReady(Input.SoftDrop))
        {
            _inputCooldown.Reset(Input.SoftDrop);
            _tetrominoManager.MoveDown();
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
