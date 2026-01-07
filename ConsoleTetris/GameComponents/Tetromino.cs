#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.

namespace ConsoleTetris.GameComponents;

internal class Tetromino
{
    private Coord[]? _piecesCoords;

    public int X { get; set { field = value; _piecesCoords = null; } }
    public int Y { get; set { field = value; _piecesCoords = null; } }
    public TetrominoShape Shape { get; init; }
    public Direction Direction { get; set { field = value; _piecesCoords = null; } }
    public Coord[] PiecesCoords => _piecesCoords ??= GetPiecesCoords();

    public Tetromino GetCopy() => new()
    {
        X = X,
        Y = Y,
        Shape = Shape,
        Direction = Direction,
    };



    private Coord[] GetPiecesCoords()
    {
        return GetRelativePiecesCoords(Shape, Direction)
            .Select(coord => new Coord(X + coord.X, Y + coord.Y))
            .ToArray();
    }

    public static Coord[] GetRelativePiecesCoords(TetrominoShape shape, Direction direction) => shape switch
    {
        TetrominoShape.I => direction switch
        {
            Direction.A or Direction.C => [new(-1, 0), new(0, 0), new(1, 0), new(2, 0)],
            Direction.D or Direction.B => [new(0, -1), new(0, 0), new(0, 1), new(0, 2)],
        },

        TetrominoShape.O => [new(0, -1), new(0, 0), new(1, -1), new(1, 0)],

        TetrominoShape.T => direction switch
        {
            Direction.A => [new(0, -1), new(-1, 0), new(0, 0), new(1, 0)],
            Direction.B => [new(0, -1), new(-1, 0), new(0, 0), new(0, 1)],
            Direction.C => [new(-1, 0), new(0, 0), new(1, 0), new(0, 1)],
            Direction.D => [new(0, -1), new(0, 0), new(1, 0), new(0, 1)],
        },

        TetrominoShape.S => direction switch
        {
            Direction.A or Direction.C => [new(-1, -1), new(0, -1), new(0, 0), new(1, 0)],
            Direction.B or Direction.D => [new(1, -1), new(0, 0), new(1, 0), new(0, 1)],
        },

        TetrominoShape.Z => direction switch
        {
            Direction.A or Direction.C => [new(0, -1), new(1, -1), new(-1, 0), new(0, 0)],
            Direction.B or Direction.D => [new(0, -1), new(0, 0), new(1, 0), new(1, 1)],
        },

        TetrominoShape.J => direction switch
        {
            Direction.A => [new(1, -1), new(-1, 0), new(0, 0), new(1, 0)],
            Direction.B => [new(-1, -1), new(0, -1), new(0, 0), new(0, 1)],
            Direction.C => [new(-1, 0), new(0, 0), new(1, 0), new(-1, 1)],
            Direction.D => [new(0, -1), new(0, 0), new(0, 1), new(1, 1)],
        },

        TetrominoShape.L => direction switch
        {
            Direction.A => [new(-1, -1), new(-1, 0), new(0, 0), new(1, 0)],
            Direction.B => [new(0, -1), new(0, 0), new(-1, 1), new(0, 1)],
            Direction.C => [new(-1, 0), new(0, 0), new(1, 0), new(1, 1)],
            Direction.D => [new(0, -1), new(1, -1), new(0, 0), new(0, 1)],
        }
    };
}
