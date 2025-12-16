namespace ConsoleTetris.Rendering;

internal static class ConsoleManager
{
    public static void InitializeConsole()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Console.SetWindowSize(GlobalConstants.WindowSize.Width, GlobalConstants.WindowSize.Height);
    }
}
