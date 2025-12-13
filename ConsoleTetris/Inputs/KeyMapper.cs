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
            ConsoleKey.A => Input.SpinLeft,
            ConsoleKey.D => Input.SpinRight,
            ConsoleKey.Spacebar => Input.Pause,
            ConsoleKey.Q => Input.Quit,
            _ => Input.Nothing,
        };
    }
}
