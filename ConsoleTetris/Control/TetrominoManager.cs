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

    public void MoveLeft()
    {
        if (_game.ActiveTetromino is null)
            return;

        var tempTetromino = _game.ActiveTetromino.GetCopy();
        tempTetromino.X--;
        if (!_game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return;

        _game.Playfield.RemovePieces(_game.ActiveTetromino.PiecesCoords);
        _game.ActiveTetromino.X--;
        _game.Playfield.AddMovingPieces(_game.ActiveTetromino);
    }

    public void MoveRight()
    {
        if (_game.ActiveTetromino is null)
            return;

        var tempTetromino = _game.ActiveTetromino.GetCopy();
        tempTetromino.X++;
        if (!_game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return;

        _game.Playfield.RemovePieces(_game.ActiveTetromino.PiecesCoords);
        _game.ActiveTetromino.X++;
        _game.Playfield.AddMovingPieces(_game.ActiveTetromino);
    }

    public void SpinLeft()
    {
        if (_game.ActiveTetromino is null)
            return;

        // TODO: Kick from wall if necessary and possible.

        var targetDirection = _game.ActiveTetromino.Direction.Previous();

        var tempTetromino = _game.ActiveTetromino.GetCopy();
        tempTetromino.Direction = targetDirection;
        if (!_game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return;

        _game.Playfield.RemovePieces(_game.ActiveTetromino.PiecesCoords);
        _game.ActiveTetromino.Direction = targetDirection;
        _game.Playfield.AddMovingPieces(_game.ActiveTetromino);
    }

    public void SpinRight()
    {
        if (_game.ActiveTetromino is null)
            return;

        // TODO: Kick from wall if necessary and possible.

        var targetDirection = _game.ActiveTetromino.Direction.Next();

        var tempTetromino = _game.ActiveTetromino.GetCopy();
        tempTetromino.Direction = targetDirection;
        if (!_game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return;

        _game.Playfield.RemovePieces(_game.ActiveTetromino.PiecesCoords);
        _game.ActiveTetromino.Direction = targetDirection;
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

        _game.Playfield.RemovePieces(tetromino.PiecesCoords);
        tetromino.Y--;
        _game.Playfield.AddMovingPieces(tetromino);
        return true;
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

    public void Lock()
    {
        if (_game.ActiveTetromino is null)
            return;

        _game.Playfield.AddLockedPieces(_game.ActiveTetromino);
        _game.ActiveTetromino = null;
    }
}
