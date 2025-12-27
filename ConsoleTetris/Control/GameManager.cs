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
            Type = TetrominoType.S, // TODO: Randomize type.
            X = PlayfieldSize.Width / 2 - 1,
            Y = 20,
            Direction = Direction.A,
        };

        // Add pieces to playfield.
        var absoluteSecondaryCoords = Tetromino.SecondaryPiecesCoords.Select(coord => (Tetromino.X + coord.x, Tetromino.Y + coord.y));
        (int x, int y)[] piecesCoords = [(Tetromino.X, Tetromino.Y), .. absoluteSecondaryCoords];
        foreach (var coord in piecesCoords)
        {
            if (coord.y >= PlayfieldSize.Height)
                continue;

            var piece = new TetrominoPiece { Type = Tetromino.Type, State = TetrominoState.Moving };
            Game.Playfield.Pieces[coord.x, coord.y] = piece;
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
        throw new NotImplementedException();
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
