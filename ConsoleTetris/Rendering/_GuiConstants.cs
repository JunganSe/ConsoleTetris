namespace ConsoleTetris.Rendering;

internal class WindowSize
{
    public const int Width = 32;
    public const int Height = 23;
}

internal class GuiPosition
{
    public const int FpsX = 25;
    public const int FpsY = 21;
    public const int ScoreX = 23;
    public const int ScoreY = 2;
    public const int HoldX = 22;
    public const int HoldY = 5;
    public const int NextX = 22;
    public const int NextY = 9;
}

internal class GuiColor
{
    public const ConsoleColor Border = ConsoleColor.White;
    public const ConsoleColor FpsText = ConsoleColor.Gray;
}

internal class PieceTexture
{
    public const string Block = "██";
    public const string Ghost = "░░";
    public const string Empty = "  ";
}