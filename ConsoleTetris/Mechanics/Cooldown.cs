namespace ConsoleTetris.Mechanics;

internal class Cooldown<T> where T : struct
{
    private readonly Dictionary<T, int> _cooldowns = [];
    private readonly Dictionary<T, int> _temporaryCooldowns = [];
    private readonly Dictionary<T, int> _elapsedFrames = [];

    /// <remarks> Call once per frame. </remarks>
    public void Update()
    {
        var keys = _cooldowns.Keys
            .Concat(_temporaryCooldowns.Keys)
            .ToHashSet();

        foreach (var key in keys)
        {
            if (_elapsedFrames[key] < GetActiveCooldown(key))
                _elapsedFrames[key]++;
        }
    }

    public void SetCooldown(T key, int frames)
    {
        _cooldowns[key] = frames;

        if (!_elapsedFrames.ContainsKey(key))
            _elapsedFrames[key] = frames; // Ensure it's ready initially.
    }

    public void SetTemporaryCooldown(T key, int frames)
    {
        _temporaryCooldowns[key] = frames;

        if (!_elapsedFrames.ContainsKey(key))
            _elapsedFrames[key] = frames; // Ensure it's ready initially.
    }

    public bool IsReady(T key)
    {
        int activeCooldown = GetActiveCooldown(key);
        return _elapsedFrames[key] >= activeCooldown;
    }

    public void Reset(T key)
    {
        if (_temporaryCooldowns.ContainsKey(key))
            _temporaryCooldowns.Remove(key);

        if (_elapsedFrames.ContainsKey(key))
            _elapsedFrames[key] = 0;
    }

    private int GetActiveCooldown(T key)
    {
        if (_temporaryCooldowns.TryGetValue(key, out int temporaryCooldown))
            return temporaryCooldown;

        _cooldowns.TryGetValue(key, out int cooldown);
        return cooldown;
    }
}