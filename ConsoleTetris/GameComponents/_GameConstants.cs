namespace ConsoleTetris.GameComponents;

internal class PlayfieldSize
{
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
    A = 0,
    B = 1,
    C = 2,
    D = 3,
}