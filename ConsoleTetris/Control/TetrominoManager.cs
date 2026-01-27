using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Control;

internal class TetrominoManager
{
    private readonly Game _game;
    private readonly TetrominoRandomizer _tetrominoRandomizer = new();
    private bool _canHold = true;

    public TetrominoShape NextShape => _tetrominoRandomizer.PeekNext();
    public TetrominoShape? HeldShape { get; private set; }

    public TetrominoManager(Game game)
    {
        _game = game;
    }

    public void MoveLeft()
    {
        TryMove(-1, 0);
    }

    public void MoveRight()
    {
        TryMove(1, 0);
    }

    public bool TryMoveDown()
    {
        return TryMove(0, -1);
    }

    /// <returns> The height dropped. </returns>
    public int HardDrop()
    {
        int height = 0;
        while (TryMove(0, -1))
        {
            height++;
        }
        return height;
    }

    public void SpinLeft()
    {
        if (_game.ActiveTetromino is null)
            return;

        var targetDirection = _game.ActiveTetromino.Direction.Previous();
        if (TryMove(0, 0, targetDirection))
            return;

        TryKick(targetDirection);
    }

    public void SpinRight()
    {
        if (_game.ActiveTetromino is null)
            return;

        var targetDirection = _game.ActiveTetromino.Direction.Next();
        if (TryMove(0, 0, targetDirection))
            return;

        TryKick(targetDirection);
    }

    private bool TryKick(Direction targetDirection)
    {
        return TryMove(0, 1, targetDirection)
            || TryMove(-1, 0, targetDirection)
            || TryMove(1, 0, targetDirection);
    }

    public bool CanMoveDown()
    {
        if (_game.ActiveTetromino is null)
            return false;

        var tempTetromino = _game.ActiveTetromino.GetCopy();
        tempTetromino.Y -= 1;
        return _game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords);
    }

    private bool TryMove(int xMod, int yMod, Direction? targetDirection = null)
    {
        if (_game.ActiveTetromino is null)
            return false;

        var tempTetromino = _game.ActiveTetromino.GetCopy();
        tempTetromino.X += xMod;
        tempTetromino.Y += yMod;
        if (targetDirection.HasValue)
            tempTetromino.Direction = targetDirection.Value;

        bool canMove = _game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords);
        if (canMove)
            Move(xMod, yMod, targetDirection);

        return canMove;
    }

    private void Move(int xMod, int yMod, Direction? targetDirection = null)
    {
        if (_game.ActiveTetromino is null)
            return;

        _game.Playfield.RemovePieces(_game.ActiveTetromino.PiecesCoords);
        _game.ActiveTetromino.X += xMod;
        _game.ActiveTetromino.Y += yMod;
        if (targetDirection.HasValue)
            _game.ActiveTetromino.Direction = targetDirection.Value;
        _game.Playfield.AddMovingPieces(_game.ActiveTetromino);
    }



    public void SpawnNext()
    {
        var nextShape = _tetrominoRandomizer.GetNext();
        Spawn(nextShape);
    }


    public void SpawnHeld()
    {
        if (HeldShape is null)
            return;

        Spawn(HeldShape.Value);
    }

    private void Spawn(TetrominoShape shape)
    {
        _game.ActiveTetromino = GetSpawnTetromino(shape);
        _game.Playfield.AddMovingPieces(_game.ActiveTetromino);
        _canHold = true;
    }

    public Tetromino GetSpawnTetromino(TetrominoShape shape) => new()
    {
        Shape = shape,
        X = TetrominoSpawnLocation.X,
        Y = TetrominoSpawnLocation.Y,
        Direction = Direction.A,
    };

    public void Lock()
    {
        if (_game.ActiveTetromino is null)
            return;

        _game.Playfield.AddLockedPieces(_game.ActiveTetromino);
        _game.ActiveTetromino = null;
    }

    public void Hold()
    {
        if (!_canHold || _game.ActiveTetromino is null)
            return;

        var activeShape = _game.ActiveTetromino.Shape;
        _game.Playfield.RemovePieces(_game.ActiveTetromino.PiecesCoords);

        if (HeldShape.HasValue)
            SpawnHeld();
        else
            SpawnNext();

        HeldShape = activeShape;
        _canHold = false;
    }

    public void UpdateGhost()
    {
        if (_game.ActiveTetromino is null)
            return;

        var tempTetromino = _game.ActiveTetromino.GetCopy();

        while (true)
        {
            tempTetromino.Y--;
            if (!_game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            {
                tempTetromino.Y++;
                break;
            }
        }

        _game.Playfield.ClearGhostPieces();
        _game.Playfield.AddGhostPieces(tempTetromino);
    }
}
