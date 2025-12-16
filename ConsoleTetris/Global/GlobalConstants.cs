namespace ConsoleTetris.Global;

internal class GlobalConstants
{
    public class WindowSize
    {
        public const int Width = PlayArea.X * 2 + PlayArea.Width * 2 + 3;
        public const int Height = PlayArea.Y + PlayArea.Height + 2;
    }

    public class PlayArea
    {
        public const int X = 2;
        public const int Y = 5;
        public const int Width = 10;
        public const int Height = 20;
    }
}
