namespace ConsoleTetris.Mechanics;

internal class Cooldowns<T> where T : struct
{
    private readonly Dictionary<T, int> _cooldowns = [];
    private readonly Dictionary<T, int> _remainingFrames = [];

    /// <remarks> Call once per frame. </remarks>
    public void Update()
    {
        foreach (var key in _remainingFrames.Keys)
        {
            if (_remainingFrames[key] > 0)
                _remainingFrames[key]--;
        }
    }

    /// <summary> Sets the standard cooldown duration. </summary>
    public void SetCooldown(T key, int frames, bool isReady = true)
    {
        _cooldowns[key] = frames;
        _remainingFrames[key] = isReady ? 0 : frames;
    }

    /// <summary> Sets a temporary cooldown that lasts until the next reset. </summary>
    public void SetTemporaryCooldown(T key, int frames)
    {
        _remainingFrames[key] = Math.Max(0, frames);
    }

    public void Reset(T key)
    {
        bool cooldownExists = _cooldowns.TryGetValue(key, out int cooldown);
        if (cooldownExists)
            _remainingFrames[key] = cooldown;
    }

    public void Ready(T key)
    {
        if (_remainingFrames.ContainsKey(key))
            _remainingFrames[key] = 0;
    }

    public bool IsReady(T key)
    {
        bool cooldownExists = _remainingFrames.TryGetValue(key, out int remainingFrames);
        return cooldownExists && remainingFrames <= 0;
    }
}
