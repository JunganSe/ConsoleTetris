namespace ConsoleTetris.Game;

internal class Tetromino
{
    public TetrominoType Type { get; init; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Direction { get; set; }
}
