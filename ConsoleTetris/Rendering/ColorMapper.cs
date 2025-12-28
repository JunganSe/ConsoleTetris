using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Rendering;

internal class ColorMapper
{
    public static ConsoleColor GetTetrominoColor(TetrominoShape shape) => shape switch
    {
        TetrominoShape.I => ConsoleColor.Cyan,
        TetrominoShape.O => ConsoleColor.Yellow,
        TetrominoShape.T => ConsoleColor.Magenta,
        TetrominoShape.S => ConsoleColor.Green,
        TetrominoShape.Z => ConsoleColor.Red,
        TetrominoShape.J => ConsoleColor.Blue,
        TetrominoShape.L => ConsoleColor.DarkYellow,
        _ => ConsoleColor.White,
    };
}
