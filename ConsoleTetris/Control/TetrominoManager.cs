using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Control;

internal class TetrominoManager
{
    private readonly Game _game;

    public TetrominoManager(Game game)
    {
        _game = game;
    }

    public void SpawnTetromino()
    {
        _game.ActiveTetromino = new Tetromino()
        {
            Shape = TetrominoShape.L, // TODO: Randomize shape.
            X = PlayfieldSize.Width / 2 - 1,
            Y = 17,
            Direction = Direction.A,
        };

        _game.Playfield.AddPieces(_game.ActiveTetromino, TetrominoState.Moving);
    }

    public void MoveTetrominoLeft()
    {
        if (_game.ActiveTetromino is null)
            return;

        var tempTetromino = _game.ActiveTetromino.GetCopy();
        tempTetromino.X--;
        if (!_game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return;

        _game.Playfield.RemovePieces(_game.ActiveTetromino.PiecesCoords);
        _game.ActiveTetromino.X--;
        _game.Playfield.AddPieces(_game.ActiveTetromino, TetrominoState.Moving);
    }

    public void MoveTetrominoRight()
    {
        if (_game.ActiveTetromino is null)
            return;

        var tempTetromino = _game.ActiveTetromino.GetCopy();
        tempTetromino.X++;
        if (!_game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return;

        _game.Playfield.RemovePieces(_game.ActiveTetromino.PiecesCoords);
        _game.ActiveTetromino.X++;
        _game.Playfield.AddPieces(_game.ActiveTetromino, TetrominoState.Moving);
    }

    public void RotateTetrominoClockwise()
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
        _game.Playfield.AddPieces(_game.ActiveTetromino, TetrominoState.Moving);
    }

    public void RotateTetrominoCounterClockwise()
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
        _game.Playfield.AddPieces(_game.ActiveTetromino, TetrominoState.Moving);
    }

    public void SoftDropTetromino()
    {
        if (_game.ActiveTetromino is null)
            return;

        TryMoveTetrominoDown(_game.ActiveTetromino);
    }

    public void HardDropTetromino()
    {
        if (_game.ActiveTetromino is null)
            return;

        bool isBottomReached = false;
        while (!isBottomReached)
        {
            isBottomReached = !TryMoveTetrominoDown(_game.ActiveTetromino);
        }
    }

    private bool TryMoveTetrominoDown(Tetromino tetromino)
    {
        var tempTetromino = tetromino.GetCopy();
        tempTetromino.Y--;
        if (!_game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return false;

        _game.Playfield.RemovePieces(tetromino.PiecesCoords);
        tetromino.Y--;
        _game.Playfield.AddPieces(tetromino, TetrominoState.Moving);
        return true;
    }

    public bool HoldTetromino()
    {
        // TODO: Implement holding logic.
        // - Check if holding is allowed.
        // - If no tetromino is held, store the active tetromino and spawn a new one.
        // - If a tetromino is held, swap it with the active tetromino and put it at the top.
        // - Store the held tetromino in a separate variable.
        // - Disable further holds until the next tetromino is locked.
        throw new NotImplementedException();
    }

    public void LockTetromino()
    {
        if (_game.ActiveTetromino is null)
            return;

        _game.Playfield.AddPieces(_game.ActiveTetromino, TetrominoState.Locked);
        _game.ActiveTetromino = null;
    }
}
