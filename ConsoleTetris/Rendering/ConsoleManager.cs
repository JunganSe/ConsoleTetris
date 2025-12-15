namespace ConsoleTetris.Rendering;

internal static class ConsoleManager
{
    public static void InitializeConsole()
    {
        Console.CursorVisible = false;
        Console.Clear();

        int width = GlobalConstants.PlayArea.X * 2 + GlobalConstants.PlayArea.Width * 2 + 3;
        int height = GlobalConstants.PlayArea.Y + GlobalConstants.PlayArea.Height + 2;
        Console.SetWindowSize(width, height);
    }
}
