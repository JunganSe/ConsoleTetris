namespace ConsoleTetris.Inputs;

internal static class KeyMapper
{
    public static Input Map(ConsoleKey key)
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
