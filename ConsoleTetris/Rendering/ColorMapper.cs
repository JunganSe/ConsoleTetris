using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Rendering;

internal class ColorMapper
{
    public static ConsoleColor GetTetrominoColor(TetrominoType tetrominoType) => tetrominoType switch
    {
        TetrominoType.I => ConsoleColor.Cyan,
        TetrominoType.O => ConsoleColor.Yellow,
        TetrominoType.T => ConsoleColor.Magenta,
        TetrominoType.S => ConsoleColor.Green,
        TetrominoType.Z => ConsoleColor.Red,
        TetrominoType.J => ConsoleColor.Blue,
        TetrominoType.L => ConsoleColor.DarkYellow,
        _ => ConsoleColor.White,
    };
}
