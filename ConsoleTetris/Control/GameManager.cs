using ConsoleTetris.GameComponents;
using ConsoleTetris.Helpers;
using ConsoleTetris.Inputs;
using ConsoleTetris.Mechanics;

namespace ConsoleTetris.Control;

internal class GameManager
{
    private readonly TetrominoManager _tetrominoManager;
    private readonly InputManager _inputManager;
    private readonly Cooldowns<Input> _inputCooldowns;
    private readonly Cooldown _gravityCooldown;
    private readonly Cooldown _lockDelayCooldown;

    public Game Game { get; }
    public TetrominoShape NextTetrominoShape => _tetrominoManager.NextShape;
    public TetrominoShape? HeldTetrominoShape => _tetrominoManager.HeldShape;

    public GameManager()
    {
        Game = new();
        _tetrominoManager = new(Game);
        _inputManager = new();

        _inputCooldowns = new();
        _gravityCooldown = new();
        _lockDelayCooldown = new(20) { IsActive = false };
        InitializeCooldowns();
    }

    private void InitializeCooldowns()
    {
        _inputCooldowns.SetCooldown(Input.Left, 3);
        _inputCooldowns.Ready(Input.Left);
        _inputCooldowns.SetCooldown(Input.Right, 3);
        _inputCooldowns.Ready(Input.Right);
        _inputCooldowns.SetCooldown(Input.SoftDrop, 2);
        _inputCooldowns.Ready(Input.SoftDrop);
        SetGravityCooldownByLevel();
    }

    /// <remarks> Call once per frame. </remarks>
    public void Update()
    {
        _inputManager.Update();
        _gravityCooldown.Update();
        _lockDelayCooldown.Update();
        _inputCooldowns.Update();
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

        if (_inputManager.InputState.IsHeld(Input.Left) && _inputCooldowns.IsReady(Input.Left))
        {
            _inputCooldowns.Reset(Input.Left);
            _tetrominoManager.MoveLeft();
        }

        if (_inputManager.InputState.IsHeld(Input.Right) && _inputCooldowns.IsReady(Input.Right))
        {
            _inputCooldowns.Reset(Input.Right);
            _tetrominoManager.MoveRight();
        }

        if (_inputManager.InputState.IsHeld(Input.SoftDrop) && _inputCooldowns.IsReady(Input.SoftDrop))
        {
            // TODO: Maybe: Lock without delay if at bottom while soft dropping.
            _inputCooldowns.Reset(Input.SoftDrop);
            if (_tetrominoManager.TryMoveDown())
                Game.Score += ScoreHelper.GetSoftDropScore(Game.Level);
        }

        if (_inputManager.InputState.IsPressed(Input.HardDrop))
        {
            int height = _tetrominoManager.HardDrop();
            _tetrominoManager.Lock();
            Game.Score += ScoreHelper.GetHardDropScore(Game.Level, height);
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

        Game.Score += ScoreHelper.GetLineClearScore(Game.Level, clearedLinesCount);
        Game.ClearedLines += clearedLinesCount;
        UpdateLevel();
        SetGravityCooldownByLevel();
    }

    private void UpdateLevel()
    {
        Game.Level = (int)Math.Floor(1 + Game.ClearedLines / 10d);
    }

    private void SetGravityCooldownByLevel()
    {
        int cooldown = GravityHelper.GetGravityCooldown(Game.Level);
        _gravityCooldown.SetCooldown(cooldown);
    }

    public bool IsGameLost()
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

    public void UpdateGhost()
    {
        _tetrominoManager.UpdateGhost();
    }
}
