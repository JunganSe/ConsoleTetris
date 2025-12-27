#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.

namespace ConsoleTetris.GameComponents;

internal class Tetromino
{
    public int X { get; set; }
    public int Y { get; set; }
    public TetrominoType Type { get; init; }
    public Direction Direction { get; set; }

    /// <summary>
    /// Gets an array of 3 coordinates for the secondary pieces,
    /// relative to the primary piece at the tetromino's coordinates.
    /// </summary>
    public (int x, int y)[] SecondaryPiecesCoords => Type switch
    {
        TetrominoType.I => Direction switch
        {
            Direction.A or Direction.C => [(-1, 0), (1, 0), (2, 0)],
            Direction.D or Direction.B => [(0, -1), (0, 1), (0, 2)],
        },

        TetrominoType.O => [(1, 0), (0, 1), (1, 1)],

        TetrominoType.T => Direction switch
        {
            Direction.A => [(0, -1), (-1, 0), (1, 0)],
            Direction.B => [(0, -1), (-1, 0), (0, 1)],
            Direction.C => [(-1, 0), (1, 0), (0, 1)],
            Direction.D => [(0, -1), (1, 0), (0, 1)],
        },

        TetrominoType.S => Direction switch
        {
            Direction.A or Direction.C => [(-1, -1), (0, -1), (1, 0)],
            Direction.B or Direction.D => [(1, -1), (1, 0), (0, 1)],
        },

        TetrominoType.Z => Direction switch
        {
            Direction.A or Direction.C => [(0, -1), (1, -1), (-1, 0)],
            Direction.B or Direction.D => [(0, -1), (1, 0), (1, 1)],
        },

        TetrominoType.J => Direction switch
        {
            Direction.A => [(1, -1), (-1, 0), (1, 0)],
            Direction.B => [(-1, -1), (0, -1), (0, 1)],
            Direction.C => [(-1, 0), (1, 0), (-1, 1)],
            Direction.D => [(0, -1), (0, 1), (1, 1)],
        },

        TetrominoType.L => Direction switch
        {
            Direction.A => [(-1, -1), (-1, 0), (1, 0)],
            Direction.B => [(0, -1), (-1, 1), (0, 1)],
            Direction.C => [(-1, 0), (1, 0), (1, 1)],
            Direction.D => [(0, -1), (1, 1), (0, 1)],
        }
    };
}
