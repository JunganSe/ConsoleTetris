namespace ConsoleTetris.Inputs;

public enum Input
{
    Unknown,
    Left,
    Right,
    HardDrop,
    SoftDrop,
    SpinLeft,
    SpinRight,
    Hold,
    Pause,
    Quit,
}

/// <summary> Virtual key codes </summary>
public enum Key
{
    Left = 0x25,
    Right = 0x27,
    Up = 0x26,
    Down = 0x28,
    A = 0x41,
    S = 0x53,
    D = 0x44,
    C = 0x43,
    Enter = 0x0D,
    Space = 0x20,
    Escape = 0x1B,
}
