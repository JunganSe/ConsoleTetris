namespace ConsoleTetris.Inputs;

internal class InputCooldown
{
    private readonly Dictionary<Input, int> _cooldowns = [];
    private readonly Dictionary<Input, int> _framesSinceInput = [];

    /// <remarks> Call once per frame. </remarks>
    public void Update()
    {
        foreach (var input in _cooldowns.Keys.ToList())
        {
            if (_framesSinceInput[input] < _cooldowns[input])
                _framesSinceInput[input]++;
        }
    }

    public void SetCooldown(Input input, int frames)
    {
        _cooldowns[input] = frames;
        if (!_framesSinceInput.ContainsKey(input))
            _framesSinceInput[input] = frames;
    }

    public bool IsReady(Input input)
    {
        return !_cooldowns.TryGetValue(input, out int value)
            || _framesSinceInput[input] >= value;
    }

    public void Reset(Input input)
    {
        if (_framesSinceInput.ContainsKey(input))
            _framesSinceInput[input] = 0;
    }
}
