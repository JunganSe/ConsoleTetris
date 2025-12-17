namespace ConsoleTetris.Inputs;

internal class InputManager
{
    private HashSet<Key> _currentHeldKeys = [];
    private HashSet<Key> _previousHeldKeys = [];

    /// <summary>
    /// Updates the state of held keys. Should be called once per frame.
    /// </summary>
    public void Update()
    {
        _previousHeldKeys = _currentHeldKeys;
        _currentHeldKeys = InputReader.GetHeldKeys<Key>();
    }

    public bool IsKeyHeld(Key key) =>
        _currentHeldKeys.Contains(key);

    public bool IsKeyPressed(Key key) =>
        _currentHeldKeys.Contains(key) && !_previousHeldKeys.Contains(key);

    public bool IsKeyReleased(Key key) =>
        !_currentHeldKeys.Contains(key) && _previousHeldKeys.Contains(key);
}
