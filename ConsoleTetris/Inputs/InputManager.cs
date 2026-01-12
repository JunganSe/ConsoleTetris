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
        _currentHeldInputs = GetHeldInputs();
        InputState = GetInputState();
    }

    private HashSet<Input> GetHeldInputs() =>
        InputReader.GetHeldKeys<Key>()
                   .Select(KeyMapper.Map)
                   .ToHashSet();

    private InputState GetInputState() => new(
        held: new HashSet<Input>(_currentHeldInputs),
        pressed: _currentHeldInputs
            .Where(input => !_previousHeldInputs.Contains(input))
            .ToHashSet(),
        released: _previousHeldInputs
            .Where(input => !_currentHeldInputs.Contains(input))
            .ToHashSet());
}
