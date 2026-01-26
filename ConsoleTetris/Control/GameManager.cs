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
    private readonly SimpleCooldown _lockDelayCooldown;

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
        UpdateGravityCooldown();
        _lockDelayCooldown = new() { IsActive = false };
        _lockDelayCooldown.SetCooldown(20);
    }

    /// <remarks> Call once per frame. </remarks>
    public void Update()
    {
        _inputManager.Update();
        _gravityCooldown.Update();
        _lockDelayCooldown.Update();
        _inputCooldown.Update();
    }

    public void HandleGravity()
    {
        if (!_gravityCooldown.IsReady())
            return;

        _gravityCooldown.Reset();
        if (!_lockDelayCooldown.IsActive && !_tetrominoManager.TryMoveDown())
        {
            _lockDelayCooldown.IsActive = true;
            _lockDelayCooldown.Reset();
        }
    }

    public void HandleLocking()
    {
        if (!_lockDelayCooldown.IsActive || !_lockDelayCooldown.IsReady())
            return;

        _lockDelayCooldown.IsActive = false;

        if (!_tetrominoManager.CanMoveDown())
            _tetrominoManager.Lock();
    }

    public void HandleInput()
    {
        // TODO: Longer cooldown after first side movement.

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
            // TODO: Maybe: Lock without delay if at bottom while soft dropping.
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
        int clearedLinesCount = Game.Playfield.ClearAllCompletedLines();
        if (clearedLinesCount == 0)
            return;

        Game.Score += _scoreManager.GetLineClearScore(Game.Level, clearedLinesCount);
        Game.ClearedLines += clearedLinesCount;
        UpdateLevel();
        UpdateGravityCooldown();
    }

    private void UpdateLevel()
    {
        Game.Level = (int)Math.Floor(1 + Game.ClearedLines / 10d);
    }

    private void UpdateGravityCooldown()
    {
        int minCooldownSoft = 2;
        int minCooldownHard = 1;
        int maxCooldown = 30;

        int cooldown = (Game.Level < 20)
            ? Math.Max(minCooldownSoft, maxCooldown - (Game.Level - 1) * 2)
            : minCooldownHard;
        _gravityCooldown.SetCooldown(cooldown);
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
