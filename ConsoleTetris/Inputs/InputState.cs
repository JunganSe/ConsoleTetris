namespace ConsoleTetris.Inputs;

internal record InputState(
    IReadOnlySet<Input> Held,
    IReadOnlySet<Input> Pressed,
    IReadOnlySet<Input> Released)
{
    public bool IsHeld(Input input) => Held.Contains(input);
    public bool IsPressed(Input input) => Pressed.Contains(input);
    public bool IsReleased(Input input) => Released.Contains(input);
}
