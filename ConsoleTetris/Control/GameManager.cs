using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Control;

internal class GameManager
{
    public Game Game { get; set; }
    public bool IsTetrominoOnBoard => Game.ActiveTetromino is not null;

    public GameManager()
    {
        Game = new();
    }

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

    public void MoveTetrominoDown()
    {
        if (Game.ActiveTetromino is null)
            return;

        var tempTetromino = new Tetromino()
        {
            Shape = Game.ActiveTetromino.Shape,
            X = Game.ActiveTetromino.X,
            Y = Game.ActiveTetromino.Y - 1,
            Direction = Game.ActiveTetromino.Direction,
        };
        if (!Game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return;

        Game.Playfield.RemovePieces(Game.ActiveTetromino);
        Game.ActiveTetromino.Y--;
        Game.Playfield.AddPieces(Game.ActiveTetromino, TetrominoState.Moving);
    }

    public void MoveTetrominoLeft()
    {
        if (Game.ActiveTetromino is null)
            return;

        var tempTetromino = new Tetromino()
        {
            Shape = Game.ActiveTetromino.Shape,
            X = Game.ActiveTetromino.X - 1,
            Y = Game.ActiveTetromino.Y,
            Direction = Game.ActiveTetromino.Direction,
        };
        if (!Game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return;

        Game.Playfield.RemovePieces(Game.ActiveTetromino);
        Game.ActiveTetromino.X--;
        Game.Playfield.AddPieces(Game.ActiveTetromino, TetrominoState.Moving);
    }

    public void MoveTetrominoRight()
    {
        if (Game.ActiveTetromino is null)
            return;

        var tempTetromino = new Tetromino()
        {
            Shape = Game.ActiveTetromino.Shape,
            X = Game.ActiveTetromino.X + 1,
            Y = Game.ActiveTetromino.Y,
            Direction = Game.ActiveTetromino.Direction,
        };
        if (!Game.Playfield.AreCoordsFree(tempTetromino.PiecesCoords))
            return;

        Game.Playfield.RemovePieces(Game.ActiveTetromino);
        Game.ActiveTetromino.X++;
        Game.Playfield.AddPieces(Game.ActiveTetromino, TetrominoState.Moving);
    }

    public void RotateTetrominoClockwise()
    {
        if (Game.ActiveTetromino is null)
            return;

        // TODO: Check if rotation is possible.
        // - Kick from wall if necessary and possible.
        // - Abort if rotation is not possible.

        Game.Playfield.RemovePieces(Game.ActiveTetromino);
        Game.ActiveTetromino.Direction = Game.ActiveTetromino.Direction.Next();
        Game.Playfield.AddPieces(Game.ActiveTetromino, TetrominoState.Moving);
    }

    public void RotateTetrominoCounterClockwise()
    {
        if (Game.ActiveTetromino is null)
            return;

        // TODO: Check if rotation is possible.
        // - Kick from wall if necessary and possible.
        // - Abort if rotation is not possible.

        Game.Playfield.RemovePieces(Game.ActiveTetromino);
        Game.ActiveTetromino.Direction = Game.ActiveTetromino.Direction.Previous();
        Game.Playfield.AddPieces(Game.ActiveTetromino, TetrominoState.Moving);
    }

    public void SoftDropTetromino()
    {
        throw new NotImplementedException();
    }

    public void HardDropTetromino()
    {
        throw new NotImplementedException();
    }

    public void LockTetromino()
    {
        // TODO: Add the pieces to playfield.
        throw new NotImplementedException();
    }
}
