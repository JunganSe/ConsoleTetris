#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.

namespace ConsoleTetris.GameComponents;

internal class Tetromino
{
    public int X { get; set; }
    public int Y { get; set; }
    public TetrominoShape Shape { get; init; }
    public Direction Direction { get; set; }
    public Coord[] PiecesCoords => GetPiecesCoords();

    public Tetromino GetCopy() => new()
    {
        X = X,
        Y = Y,
        Shape = Shape,
        Direction = Direction,
    };



    /// <summary> Gets an array of coordinates for the secondary pieces. </summary>
    private Coord[] GetPiecesCoords()
    {
        var secondaryPiecesCoords = GetRelativeSecondaryPiecesCoords()
            .Select(coord => new Coord(X + coord.X, Y + coord.Y));
        return [new Coord(X, Y), .. secondaryPiecesCoords];
    }

    /// <summary>
    /// Gets an array of coordinates for the secondary pieces,
    /// relative to the primary piece at the tetromino's coordinates.
    /// </summary>
    private Coord[] GetRelativeSecondaryPiecesCoords() => Shape switch
    {
        TetrominoShape.I => Direction switch
        {
            Direction.A or Direction.C => [new(-1, 0), new(1, 0), new(2, 0)],
            Direction.D or Direction.B => [new(0, -1), new(0, 1), new(0, 2)],
        },

        TetrominoShape.O => [new(0, -1), new(1, -1), new(1, 0)],

        TetrominoShape.T => Direction switch
        {
            Direction.A => [new(0, -1), new(-1, 0), new(1, 0)],
            Direction.B => [new(0, -1), new(-1, 0), new(0, 1)],
            Direction.C => [new(-1, 0), new(1, 0), new(0, 1)],
            Direction.D => [new(0, -1), new(1, 0), new(0, 1)],
        },

        TetrominoShape.S => Direction switch
        {
            Direction.A or Direction.C => [new(-1, -1), new(0, -1), new(1, 0)],
            Direction.B or Direction.D => [new(1, -1), new(1, 0), new(0, 1)],
        },

        TetrominoShape.Z => Direction switch
        {
            Direction.A or Direction.C => [new(0, -1), new(1, -1), new(-1, 0)],
            Direction.B or Direction.D => [new(0, -1), new(1, 0), new(1, 1)],
        },

        TetrominoShape.J => Direction switch
        {
            Direction.A => [new(1, -1), new(-1, 0), new(1, 0)],
            Direction.B => [new(-1, -1), new(0, -1), new(0, 1)],
            Direction.C => [new(-1, 0), new(1, 0), new(-1, 1)],
            Direction.D => [new(0, -1), new(0, 1), new(1, 1)],
        },

        TetrominoShape.L => Direction switch
        {
            Direction.A => [new(-1, -1), new(-1, 0), new(1, 0)],
            Direction.B => [new(0, -1), new(-1, 1), new(0, 1)],
            Direction.C => [new(-1, 0), new(1, 0), new(1, 1)],
            Direction.D => [new(0, -1), new(1, -1), new(0, 1)],
        }
    };
}
