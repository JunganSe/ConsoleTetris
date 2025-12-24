namespace ConsoleTetris.GameComponents;

internal class PlayfieldSize
{
    public const int X = 1;
    public const int Y = 1;
    public const int Width = 10;
    public const int Height = 20;
}

internal enum TetrominoType
{
    I,
    O,
    T,
    S,
    Z,
    J,
    L,
}

internal enum TetrominoState
{
    Moving,
    Locked,
    Ghost,
}

internal enum Direction
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3,
}