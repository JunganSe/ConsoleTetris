namespace ConsoleTetris.Inputs;

internal static class InputReader_Single
{
    public static Input ReadInput()
    {
        var key = ConsoleKey.None;
        while (Console.KeyAvailable)
        {
            key = Console.ReadKey(true).Key;
        }
        return MapKey(key);
    }

    private static Input MapKey(ConsoleKey key)
    {
        return key switch
        {
            ConsoleKey.LeftArrow => Input.Left,
            ConsoleKey.RightArrow => Input.Right,
            ConsoleKey.UpArrow => Input.Up,
            ConsoleKey.DownArrow => Input.Down,
            ConsoleKey.Q => Input.Quit,
            ConsoleKey.Spacebar => Input.Pause,
            _ => Input.Nothing,
        };
    }
}
