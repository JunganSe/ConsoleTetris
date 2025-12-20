namespace ConsoleTetris.Inputs;

internal static class KeyMapper
{
    public static Input Map(Key key) => key switch
    {
        Key.Left   => Input.Left,
        Key.Right  => Input.Right,
        Key.Up     => Input.Up,
        Key.Down   => Input.Down,
        Key.A      => Input.SpinLeft,
        Key.D      => Input.SpinRight,
        Key.Space  => Input.Pause,
        Key.Escape => Input.Quit,
        _          => Input.Unknown,
    };
}
