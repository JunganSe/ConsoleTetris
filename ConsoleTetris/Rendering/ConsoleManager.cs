namespace ConsoleTetris.Rendering;

internal static class ConsoleManager
{
    public static void InitializeConsole()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Console.Title = "Tetris";
        Console.SetWindowSize(WindowSize.Width, WindowSize.Height);
    }
}
