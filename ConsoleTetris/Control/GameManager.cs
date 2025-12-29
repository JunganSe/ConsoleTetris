using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Control;

internal class GameManager
{
    public Game Game { get; set; }
    public Tetromino? Tetromino { get; private set; }
    public bool IsTetrominoOnBoard => Tetromino is not null;

    public GameManager()
    {
        Game = new();
    }

    public void SpawnTetromino()
    {
        Tetromino = new Tetromino
        {
            Shape = TetrominoShape.L, // TODO: Randomize shape.
            X = PlayfieldSize.Width / 2 - 1,
            Y = 17,
            Direction = Direction.A,
        };

        Game.Playfield.AddPieces(Tetromino, TetrominoState.Moving);
    }

    public void MoveTetrominoDown()
    {
        if (Tetromino is null)
            return;

        // TODO: Check if movement is possible.

        Game.Playfield.RemovePieces(Tetromino);
        Tetromino.Y--;
        Game.Playfield.AddPieces(Tetromino, TetrominoState.Moving);
    }

    public void MoveTetrominoLeft()
    {
        if (Tetromino is null)
            return;

        // TODO: Check if movement is possible.

        Game.Playfield.RemovePieces(Tetromino);
        Tetromino.X--;
        Game.Playfield.AddPieces(Tetromino, TetrominoState.Moving);
    }

    public void MoveTetrominoRight()
    {
        if (Tetromino is null)
            return;

        // TODO: Check if movement is possible.

        Game.Playfield.RemovePieces(Tetromino);
        Tetromino.X++;
        Game.Playfield.AddPieces(Tetromino, TetrominoState.Moving);
    }

    public void RotateTetrominoClockwise()
    {
        if (Tetromino is null)
            return;

        // TODO: Check if rotation is possible.
        // - Kick from wall if necessary and possible.
        // - Abort if rotation is not possible.

        Game.Playfield.RemovePieces(Tetromino);
        Tetromino.Direction = Tetromino.Direction.Next();
        Game.Playfield.AddPieces(Tetromino, TetrominoState.Moving);
    }

    public void RotateTetrominoCounterClockwise()
    {
        if (Tetromino is null)
            return;

        // TODO: Check if rotation is possible.
        // - Kick from wall if necessary and possible.
        // - Abort if rotation is not possible.

        Game.Playfield.RemovePieces(Tetromino);
        Tetromino.Direction = Tetromino.Direction.Previous();
        Game.Playfield.AddPieces(Tetromino, TetrominoState.Moving);
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
