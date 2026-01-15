using ConsoleTetris.GameComponents;
using ConsoleTetris.Inputs;
using ConsoleTetris.Mechanics;

namespace ConsoleTetris.Control;

internal class GameManager
{
    private readonly TetrominoManager _tetrominoManager;
    private readonly InputManager _inputManager;
    private readonly Cooldown<Input> _inputCooldown;
    private readonly SimpleCooldown _gravityCooldown;

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
        _gravityCooldown = new();
        _gravityCooldown.SetCooldown(30);
    }

    public void UpdateInput()
    {
        _inputManager.Update();
    }

    public void MoveDownOrLockOnTimer()
    {
        _gravityCooldown.Update();
        if (!_gravityCooldown.IsReady())
            return;

        _gravityCooldown.Reset();
        if(!_tetrominoManager.TryMoveDown())
            _tetrominoManager.Lock();
    }

    public void HandleInput()
    {
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

        if (_inputManager.InputState.IsPressed(Input.SpinLeft))
            _tetrominoManager.SpinLeft();

        if (_inputManager.InputState.IsPressed(Input.SpinRight))
            _tetrominoManager.SpinRight();

        if (_inputManager.InputState.IsPressed(Input.Hold))
            _tetrominoManager.Hold();

        if (_inputManager.InputState.IsPressed(Input.Pause))
            _tetrominoManager.Lock(); // Temporary for testing.
    }

    public void SpawnTetrominoIfApplicable()
    {
        if (Game.ActiveTetromino is null)
        {
            _tetrominoManager.SpawnNext();
            _gravityCooldown.Reset();
        }
    }
}
