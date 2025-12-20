namespace ConsoleTetris.Inputs;

internal class InputManager
{
    private HashSet<Input> _currentHeldInputs = [];
    private HashSet<Input> _previousHeldInputs = [];

    /// <summary>
    /// Updates the state of held inputs. Should be called once per frame.
    /// </summary>
    public void Update()
    {
        _previousHeldInputs = _currentHeldInputs;
        _currentHeldInputs = InputReader.GetHeldKeys<Key>()
                                        .Select(KeyMapper.Map)
                                        .ToHashSet();
    }

    public bool IsHeld(Input input) =>
        _currentHeldInputs.Contains(input);

    public bool IsPressed(Input input) =>
        _currentHeldInputs.Contains(input) && !_previousHeldInputs.Contains(input);

    public bool IsReleased(Input input) =>
        !_currentHeldInputs.Contains(input) && _previousHeldInputs.Contains(input);
}
