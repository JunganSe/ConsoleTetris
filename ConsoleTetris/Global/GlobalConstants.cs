namespace ConsoleTetris.Global;

internal class GlobalConstants
{
    public class WindowSize
    {
        public const int Width = Playfield.X * 2 + Playfield.Width * 2 + 3;
        public const int Height = Playfield.Y + Playfield.Height + 2;
    }

    public class Playfield
    {
        public const int X = 2;
        public const int Y = 5;
        public const int Width = 10;
        public const int Height = 20;
    }
}
