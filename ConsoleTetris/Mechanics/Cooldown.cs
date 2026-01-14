namespace ConsoleTetris.Mechanics;

internal class Cooldown<T> where T : struct
{
    private readonly Dictionary<T, int> _cooldowns = [];
    private readonly Dictionary<T, int> _elapsedFrames = [];

    /// <remarks> Call once per frame. </remarks>
    public void Update()
    {
        foreach (var key in _cooldowns.Keys.ToList())
        {
            if (_elapsedFrames[key] < _cooldowns[key])
                _elapsedFrames[key]++;
        }
    }

    public void SetCooldown(T key, int frames)
    {
        _cooldowns[key] = frames;
        if (!_elapsedFrames.ContainsKey(key))
            _elapsedFrames[key] = frames;
    }

    public bool IsReady(T key)
    {
        return !_cooldowns.TryGetValue(key, out int value)
            || _elapsedFrames[key] >= value;
    }

    public void Reset(T key)
    {
        if (_elapsedFrames.ContainsKey(key))
            _elapsedFrames[key] = 0;
    }
}
