#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.

namespace ConsoleTetris.GameComponents;

internal class Tetromino
{
    public int X { get; set; }
    public int Y { get; set; }
    public TetrominoShape Shape { get; init; }
    public Direction Direction { get; set; }
    public (int x, int y)[] PiecesCoords => GetPiecesCoords();



    /// <summary> Gets an array of coordinates for the secondary pieces. </summary>
    private (int x, int y)[] GetPiecesCoords()
    {
        var secondaryPiecesCoords = GetRelativeSecondaryPiecesCoords()
            .Select(coord => (X + coord.x, Y + coord.y));
        return [(X, Y), .. secondaryPiecesCoords];
    }

    /// <summary>
    /// Gets an array of coordinates for the secondary pieces,
    /// relative to the primary piece at the tetromino's coordinates.
    /// </summary>
    private (int x, int y)[] GetRelativeSecondaryPiecesCoords() => Shape switch
    {
        TetrominoShape.I => Direction switch
        {
            Direction.A or Direction.C => [(-1, 0), (1, 0), (2, 0)],
            Direction.D or Direction.B => [(0, -1), (0, 1), (0, 2)],
        },

        TetrominoShape.O => [(1, 0), (0, 1), (1, 1)],

        TetrominoShape.T => Direction switch
        {
            Direction.A => [(0, -1), (-1, 0), (1, 0)],
            Direction.B => [(0, -1), (-1, 0), (0, 1)],
            Direction.C => [(-1, 0), (1, 0), (0, 1)],
            Direction.D => [(0, -1), (1, 0), (0, 1)],
        },

        TetrominoShape.S => Direction switch
        {
            Direction.A or Direction.C => [(-1, -1), (0, -1), (1, 0)],
            Direction.B or Direction.D => [(1, -1), (1, 0), (0, 1)],
        },

        TetrominoShape.Z => Direction switch
        {
            Direction.A or Direction.C => [(0, -1), (1, -1), (-1, 0)],
            Direction.B or Direction.D => [(0, -1), (1, 0), (1, 1)],
        },

        TetrominoShape.J => Direction switch
        {
            Direction.A => [(1, -1), (-1, 0), (1, 0)],
            Direction.B => [(-1, -1), (0, -1), (0, 1)],
            Direction.C => [(-1, 0), (1, 0), (-1, 1)],
            Direction.D => [(0, -1), (0, 1), (1, 1)],
        },

        TetrominoShape.L => Direction switch
        {
            Direction.A => [(-1, -1), (-1, 0), (1, 0)],
            Direction.B => [(0, -1), (-1, 1), (0, 1)],
            Direction.C => [(-1, 0), (1, 0), (1, 1)],
            Direction.D => [(0, -1), (1, -1), (0, 1)],
        }
    };
}
