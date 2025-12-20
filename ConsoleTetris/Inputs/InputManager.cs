namespace ConsoleTetris.Inputs;

internal class InputManager
{
    private HashSet<Input> _currentHeldInputs = [];
    private HashSet<Input> _previousHeldInputs = [];

    /// <summary> Updates the state of held inputs. Should be called once per frame. </summary>
    public void Update()
    {
        _previousHeldInputs = [.. _currentHeldInputs];
        _currentHeldInputs = InputReader.GetHeldKeys<Key>()
                                        .Select(KeyMapper.Map)
                                        .ToHashSet();
    }

    /// <summary> Gets a snapshot of the current input state. </summary>
    /// <remarks> This is typically called after updating the input state with <see cref="Update"/>. </remarks>
    public InputState GetInputState()
    {
        var pressedInputs = _currentHeldInputs
            .Where(input => !_previousHeldInputs.Contains(input))
            .ToHashSet();
        var releasedInputs = _previousHeldInputs
            .Where(input => !_currentHeldInputs.Contains(input))
            .ToHashSet();
        return new InputState(
            Held: new HashSet<Input>(_currentHeldInputs),
            Pressed: pressedInputs,
            Released: releasedInputs);
    }
}
