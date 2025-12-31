using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Control;

internal class GameManager
{
    public Game Game { get; } = new();
    public bool IsTetrominoOnBoard => Game.ActiveTetromino is not null;

    public void SpawnTetromino()
    {
        Game.ActiveTetromino = new Tetromino()
        {
            Shape = TetrominoShape.L, // TODO: Randomize shape.
            X = PlayfieldSize.Width / 2 - 1,
            Y = 17,
            Direction = Direction.A,
        };

        Game.Playfield.AddPieces(Game.ActiveTetromino, TetrominoState.Moving);
    }

    public void MoveTetrominoLeft()
    {
        if (Game.ActiveTetromino is null)
            return;

        var tempTetromino = Game.ActiveTetromino.GetCopy();
        tempTetromino.X--;
        if (!Game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return;

        Game.Playfield.RemovePieces(Game.ActiveTetromino.PiecesCoords);
        Game.ActiveTetromino.X--;
        Game.Playfield.AddPieces(Game.ActiveTetromino, TetrominoState.Moving);
    }

    public void MoveTetrominoRight()
    {
        if (Game.ActiveTetromino is null)
            return;

        var tempTetromino = Game.ActiveTetromino.GetCopy();
        tempTetromino.X++;
        if (!Game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return;

        Game.Playfield.RemovePieces(Game.ActiveTetromino.PiecesCoords);
        Game.ActiveTetromino.X++;
        Game.Playfield.AddPieces(Game.ActiveTetromino, TetrominoState.Moving);
    }

    public void RotateTetrominoClockwise()
    {
        if (Game.ActiveTetromino is null)
            return;

        // TODO: Kick from wall if necessary and possible.

        var targetDirection = Game.ActiveTetromino.Direction.Next();

        var tempTetromino = Game.ActiveTetromino.GetCopy();
        tempTetromino.Direction = targetDirection;
        if (!Game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return;

        Game.Playfield.RemovePieces(Game.ActiveTetromino.PiecesCoords);
        Game.ActiveTetromino.Direction = targetDirection;
        Game.Playfield.AddPieces(Game.ActiveTetromino, TetrominoState.Moving);
    }

    public void RotateTetrominoCounterClockwise()
    {
        if (Game.ActiveTetromino is null)
            return;

        // TODO: Kick from wall if necessary and possible.

        var targetDirection = Game.ActiveTetromino.Direction.Previous();

        var tempTetromino = Game.ActiveTetromino.GetCopy();
        tempTetromino.Direction = targetDirection;
        if (!Game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return;

        Game.Playfield.RemovePieces(Game.ActiveTetromino.PiecesCoords);
        Game.ActiveTetromino.Direction = targetDirection;
        Game.Playfield.AddPieces(Game.ActiveTetromino, TetrominoState.Moving);
    }

    public void SoftDropTetromino()
    {
        if (Game.ActiveTetromino is null)
            return;

        TryMoveTetrominoDown(Game.ActiveTetromino);
    }

    public void HardDropTetromino()
    {
        if (Game.ActiveTetromino is null)
            return;

        do
        { }
        while (TryMoveTetrominoDown(Game.ActiveTetromino));

        // TODO: Lock the tetromino immediately after hard drop.
    }

    private bool TryMoveTetrominoDown(Tetromino tetromino)
    {
        var tempTetromino = tetromino.GetCopy();
        tempTetromino.Y--;
        if (!Game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return false;

        Game.Playfield.RemovePieces(tetromino.PiecesCoords);
        tetromino.Y--;
        Game.Playfield.AddPieces(tetromino, TetrominoState.Moving);
        return true;
    }

    public void LockTetromino()
    {
        // TODO: Add the pieces to playfield and remove the active tetromino.
        throw new NotImplementedException();
    }
}
