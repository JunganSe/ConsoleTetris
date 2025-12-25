using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Control;

internal class GameManager
{
    public Game Game { get; set; } = new();
    public Tetromino? Tetromino { get; private set; }
    public bool IsTetrominoOnBoard => Tetromino is not null;

    public GameManager()
    {
        
    }

    public void SpawnTetromino()
    {
        throw new NotImplementedException();
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
}
