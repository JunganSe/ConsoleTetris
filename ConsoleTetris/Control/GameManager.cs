using ConsoleTetris.GameComponents;
using ConsoleTetris.Inputs;
using ConsoleTetris.Mechanics;

namespace ConsoleTetris.Control;

internal class GameManager
{
    private readonly TetrominoManager _tetrominoManager;
    private readonly InputManager _inputManager;
    private readonly ScoreManager _scoreManager;
    private readonly Cooldown<Input> _inputCooldown;
    private readonly SimpleCooldown _gravityCooldown;
    private readonly SimpleCooldown _lockGraceCooldown;

    public Game Game { get; }
    public TetrominoShape NextTetrominoShape => _tetrominoManager.NextShape;
    public TetrominoShape? HeldTetrominoShape => _tetrominoManager.HeldShape;

    public GameManager()
    {
        Game = new();
        _tetrominoManager = new(Game);
        _inputManager = new();
        _scoreManager = new();
        _inputCooldown = new();
        _inputCooldown.SetCooldown(Input.Left, 3);
        _inputCooldown.SetCooldown(Input.Right, 3);
        _inputCooldown.SetCooldown(Input.SoftDrop, 2);
        _gravityCooldown = new();
        _gravityCooldown.SetCooldown(30);
        _lockGraceCooldown = new() { IsActive = false };
        _lockGraceCooldown.SetCooldown(20);
    }

    /// <remarks> Call once per frame. </remarks>
    public void Update()
    {
        _inputManager.Update();
        _gravityCooldown.Update();
        _lockGraceCooldown.Update();
        _inputCooldown.Update();
    }

    public void HandleGravity()
    {
        if (!_gravityCooldown.IsReady())
            return;

        _gravityCooldown.Reset();
        if (!_lockGraceCooldown.IsActive && !_tetrominoManager.TryMoveDown())
        {
            _lockGraceCooldown.IsActive = true;
            _lockGraceCooldown.Reset();
        }
    }

    public void HandleLocking()
    {
        if (!_lockGraceCooldown.IsActive || !_lockGraceCooldown.IsReady())
            return;

        _lockGraceCooldown.IsActive = false;

        if (!_tetrominoManager.CanMoveDown())
            _tetrominoManager.Lock();
    }

    public void HandleInput()
    {
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
            if (_tetrominoManager.TryMoveDown())
                Game.Score += _scoreManager.GetSoftDropScore(Game.Level);
        }

        if (_inputManager.InputState.IsPressed(Input.HardDrop))
        {
            int height = _tetrominoManager.HardDrop();
            _tetrominoManager.Lock();
            Game.Score += _scoreManager.GetHardDropScore(Game.Level, height);
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

    /// <summary> Clears completed lines and awards score. </summary>
    public void HandleCompletedLines()
    {
        // TODO: Refactor, handle this loop in playfield.
        int clearedLinesCount = 0;
        for (int y = 0; y < PlayfieldSize.Height; y++)
        {
            if (Game.Playfield.IsLineComplete(y))
            {
                Game.Playfield.ClearLine(y);
                Game.Playfield.MoveLinesDown(y + 1);
                clearedLinesCount++;
            }
        }

        Game.Score += _scoreManager.GetClearScore(Game.Level, clearedLinesCount);
        Game.ClearedLines += clearedLinesCount;
    }

    public void UpdateLevel()
    {
        Game.Level = (int)Math.Floor(1 + Game.ClearedLines / 10d);
    }

    public bool CheckForLoss()
    {
        if (Game.ActiveTetromino is not null)
            return false;

        var tempTetromino = _tetrominoManager.GetSpawnTetromino(_tetrominoManager.NextShape);
        bool areCoordsFree = Game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords);
        return !areCoordsFree;
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
