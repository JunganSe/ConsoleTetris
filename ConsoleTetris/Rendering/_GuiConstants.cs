namespace ConsoleTetris.Rendering;

internal abstract class WindowSize
{
    public const int Width = 31;
    public const int Height = 22;
}

internal abstract class GuiPosition
{
    public const int PlayfieldX = 1;
    public const int PlayfieldY = 1;
    public const int ScoreX = 23;
    public const int ScoreY = 2;
    public const int HoldX = 22;
    public const int HoldY = 5;
    public const int NextX = 22;
    public const int NextY = 10;
    public const int NextWith = 4;
    public const int NextHeight = 2;
    public const int FpsX = 25;
    public const int FpsY = 21;
}

internal abstract class GuiColor
{
    public const ConsoleColor Border = ConsoleColor.White;
    public const ConsoleColor FpsText = ConsoleColor.Gray;
}

internal abstract class PieceTexture
{
    public const string Block = "██";
    public const string Ghost = "░░";
    public const string Empty = "  ";
}