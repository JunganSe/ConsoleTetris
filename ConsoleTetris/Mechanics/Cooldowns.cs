namespace ConsoleTetris.Mechanics;

internal class Cooldowns<T> where T : struct
{
    private class CooldownData(int duration, int remainingFrames)
    {
        public int Duration = duration;
        public int RemainingFrames = remainingFrames;
    }

    private readonly Dictionary<T, CooldownData> _cooldowns = [];

    /// <remarks> Call once per frame. </remarks>
    public void Update()
    {
        foreach (var key in _cooldowns.Keys)
        {
            var cooldown = _cooldowns[key];
            if (cooldown.RemainingFrames > 0)
                cooldown.RemainingFrames--;
        }
    }

    /// <summary> Sets the standard cooldown duration. </summary>
    public void SetCooldown(T key, int frames, bool isReady = true)
    {
        int clampedFrames = Math.Max(0, frames);
        int remainingFrames = isReady ? 0 : clampedFrames;
        _cooldowns[key] = new CooldownData(clampedFrames, remainingFrames);
    }

    /// <summary> Sets a temporary cooldown that lasts until the next reset. </summary>
    /// <remarks> A cooldown must already exist for the key. </remarks>
    public void SetTemporaryCooldown(T key, int frames)
    {
        if (_cooldowns.TryGetValue(key, out CooldownData? cooldownData))
            cooldownData.RemainingFrames = Math.Max(0, frames);
    }

    public void Reset(T key)
    {
        if (_cooldowns.TryGetValue(key, out CooldownData? cooldownData))
            cooldownData.RemainingFrames = cooldownData.Duration;
    }

    public void Ready(T key)
    {
        if (_cooldowns.TryGetValue(key, out CooldownData? cooldownData))
            cooldownData.RemainingFrames = 0;
    }

    public bool IsReady(T key)
    {
        return _cooldowns.TryGetValue(key, out CooldownData? cooldownData)
            && cooldownData.RemainingFrames <= 0;
    }
}
