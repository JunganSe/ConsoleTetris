namespace ConsoleTetris.Inputs;

internal record InputState
{
    private readonly IReadOnlySet<Input> _held;
    private readonly IReadOnlySet<Input> _pressed;
    private readonly IReadOnlySet<Input> _released;

    public static readonly InputState Empty = new(
        new HashSet<Input>(),
        new HashSet<Input>(),
        new HashSet<Input>());

    public InputState(
        IReadOnlySet<Input> held,
        IReadOnlySet<Input> pressed,
        IReadOnlySet<Input> released)
    {
        _held = held;
        _pressed = pressed;
        _released = released;
    }

    public bool IsHeld(Input input) => _held.Contains(input);
    public bool IsPressed(Input input) => _pressed.Contains(input);
    public bool IsReleased(Input input) => _released.Contains(input);
}
