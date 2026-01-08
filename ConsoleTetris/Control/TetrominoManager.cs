using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Control;

internal class TetrominoManager
{
    private readonly Game _game;
    private readonly TetrominoRandomizer _tetrominoRandomizer = new();

    public TetrominoShape NextShape => _tetrominoRandomizer.PeekNext();

    public TetrominoManager(Game game)
    {
        _game = game;
    }

    public void MoveLeft()
    {
        if (_game.ActiveTetromino is null)
            return;

        var tempTetromino = _game.ActiveTetromino.GetCopy();
        tempTetromino.X--;
        if (!_game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return;

        Move(-1, 0);
    }

    public void MoveRight()
    {
        if (_game.ActiveTetromino is null)
            return;

        var tempTetromino = _game.ActiveTetromino.GetCopy();
        tempTetromino.X++;
        if (!_game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return;

        Move(1, 0);
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

    private bool TryMove(int xMod, int yMod, Direction targetDirection)
    {
        if (_game.ActiveTetromino is null)
            return false;

        var tempTetromino = _game.ActiveTetromino.GetCopy();
        tempTetromino.X += xMod;
        tempTetromino.Y += yMod;
        tempTetromino.Direction = targetDirection;
        bool canMove = _game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords);
        if (canMove)
            Move(-1, 0, targetDirection);

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



    public void SoftDrop()
    {
        if (_game.ActiveTetromino is null)
            return;

        TryMoveDown(_game.ActiveTetromino);
    }

    public void HardDrop()
    {
        if (_game.ActiveTetromino is null)
            return;

        bool isBottomReached = false;
        while (!isBottomReached)
        {
            isBottomReached = !TryMoveDown(_game.ActiveTetromino);
        }
    }

    private bool TryMoveDown(Tetromino tetromino)
    {
        var tempTetromino = tetromino.GetCopy();
        tempTetromino.Y--;
        if (!_game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return false;

        Move(0, -1);
        return true;
    }



    public void Spawn()
    {
        _game.ActiveTetromino = new Tetromino()
        {
            Shape = _tetrominoRandomizer.GetNext(),
            X = PlayfieldSize.Width / 2 - 2,
            Y = PlayfieldSize.Height - 2,
            Direction = Direction.A,
        };

        _game.Playfield.AddMovingPieces(_game.ActiveTetromino);
    }

    public void Lock()
    {
        if (_game.ActiveTetromino is null)
            return;

        _game.Playfield.AddLockedPieces(_game.ActiveTetromino);
        _game.ActiveTetromino = null;
    }

    public bool Hold()
    {
        // TODO: Implement holding logic.
        // - Check if holding is allowed.
        // - If no tetromino is held, store the active tetromino and spawn a new one.
        // - If a tetromino is held, swap it with the active tetromino and put it at the top.
        // - Store the held tetromino in a separate variable.
        // - Disable further holds until the next tetromino is locked.
        throw new NotImplementedException();
    }
}
