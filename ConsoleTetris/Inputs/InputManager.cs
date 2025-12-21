namespace ConsoleTetris.Inputs;

internal class InputManager
{
    private HashSet<Input> _currentHeldInputs = [];
    private HashSet<Input> _previousHeldInputs = [];

    public InputState InputState { get; private set; } = InputState.Empty;

    /// <summary> Updates the state of inputs. Should be called once per frame. </summary>
    /// <remarks> Inputs are stored in the <see cref="InputState"/> property. </remarks>
    public void Update()
    {
        _previousHeldInputs = [.. _currentHeldInputs];
        _currentHeldInputs = InputReader.GetHeldKeys<Key>()
                                        .Select(KeyMapper.Map)
                                        .ToHashSet();
        InputState = GetInputState();
    }

    private InputState GetInputState() => new(
        Held: new HashSet<Input>(_currentHeldInputs),
        Pressed: _currentHeldInputs
            .Where(input => !_previousHeldInputs.Contains(input))
            .ToHashSet(),
        Released: _previousHeldInputs
            .Where(input => !_currentHeldInputs.Contains(input))
            .ToHashSet());
}
