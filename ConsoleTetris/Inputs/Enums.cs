namespace ConsoleTetris.Inputs;

public enum Input
{
    Unknown,
    Up,
    Down,
    Left,
    Right,
    SpinLeft,
    SpinRight,
    Pause,
    Quit,
}

/// <summary> Virtual key codes </summary>
public enum Key
{
    Up = 0x26,
    Down = 0x28,
    Left = 0x25,
    Right = 0x27,
    A = 0x41,
    S = 0x53,
    D = 0x44,
    Enter = 0x0D,
    Space = 0x20,
    Escape = 0x1B,
}
