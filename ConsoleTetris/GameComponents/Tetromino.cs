namespace ConsoleTetris.GameComponents;

internal class Tetromino
{
    public TetrominoType Type { get; init; }
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set; }
}
