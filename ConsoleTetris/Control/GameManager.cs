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
        // Add new Tetromino at start location.
        Tetromino = new Tetromino
        {
            Type = TetrominoType.L, // TODO: Randomize type.
            X = PlayfieldSize.Width / 2 - 1,
            Y = 17,
            Direction = Direction.A,
        };

        // Add pieces to playfield.
        foreach (var (x, y) in Tetromino.PiecesCoords)
        {
            if (y >= PlayfieldSize.Height)
                continue;

            var piece = new TetrominoPiece { Type = Tetromino.Type, State = TetrominoState.Moving };
            Game.Playfield.Pieces[x, y] = piece;
        }
    }

    public void MoveTetrominoDown()
    {
        throw new NotImplementedException();
    }

    public void MoveTetrominoLeft()
    {
        throw new NotImplementedException();
    }

    public void MoveTetrominoRight()
    {
        throw new NotImplementedException();
    }

    public void RotateTetrominoClockwise()
    {
        if (Tetromino is null)
            return;

        // TODO: Check if rotation is possible.
        // - Kick from wall if necessary and possible.
        // - Abort if rotation is not possible.

        foreach (var coord in Tetromino.SecondaryPiecesCoords)
        {
            if (coord.y >= PlayfieldSize.Height)
                continue;

            Game.Playfield.Pieces[coord.x, coord.y] = null;
        }

        Tetromino.Direction = Tetromino.Direction.Next();
        foreach (var coord in Tetromino.SecondaryPiecesCoords)
        {
            if (coord.y >= PlayfieldSize.Height)
                continue;

            var piece = new TetrominoPiece { Type = Tetromino.Type, State = TetrominoState.Moving };
            Game.Playfield.Pieces[coord.x, coord.y] = piece;
        }
    }

    public void RotateTetrominoCounterClockwise()
    {
        throw new NotImplementedException();
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
