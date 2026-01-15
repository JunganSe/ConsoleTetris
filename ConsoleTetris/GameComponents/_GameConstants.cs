namespace ConsoleTetris.GameComponents;

internal abstract class PlayfieldSize
{
    public const int Width = 10;
    public const int Height = 20;
}

internal enum TetrominoShape
{
    I,
    O,
    T,
    S,
    Z,
    J,
    L,
}

internal enum TetrominoPieceState
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

internal static class DirectionExtensions
{
    public static Direction Next(this Direction currentDirection) =>
        (Direction)(((int)currentDirection + 1) % 4);

    public static Direction Previous(this Direction currentDirection) =>
        (Direction)(((int)currentDirection - 1 + 4) % 4);
}