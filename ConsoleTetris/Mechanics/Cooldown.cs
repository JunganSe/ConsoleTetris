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
            if (_elapsedFrames[key] < GetHighestCooldown(key))
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
        int highestCooldown = GetHighestCooldown(key);
        return _elapsedFrames[key] >= highestCooldown;
    }

    public void Reset(T key)
    {
        if (_elapsedFrames.ContainsKey(key))
            _elapsedFrames[key] = 0;

        if (_temporaryCooldowns.ContainsKey(key))
            _temporaryCooldowns.Remove(key);
    }



    private int GetHighestCooldown(T key)
    {
        _cooldowns.TryGetValue(key, out int cooldown);
        _temporaryCooldowns.TryGetValue(key, out int tempCooldown);
        return Math.Max(cooldown, tempCooldown);
    }
}
