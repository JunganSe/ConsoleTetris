namespace ConsoleTetris.Mechanics;

internal class Cooldown
{
    private int _cooldown;
    private int _remainingFrames;

    public bool IsActive { get; set; } = true;

    public Cooldown(int frames = 0, bool isReady = true)
    {
        _cooldown = Math.Max(0, frames);
        _remainingFrames = isReady ? 0 : frames;
    }

    /// <remarks> Call once per frame. </remarks>
    public void Update()
    {
        if (IsActive && _remainingFrames > 0)
            _remainingFrames--;
    }

    /// <summary> Sets the standard cooldown duration. </summary>
    public void SetCooldown(int frames) =>
        _cooldown = Math.Max(0, frames);

    /// <summary> Sets a temporary cooldown that lasts until the next reset. </summary>
    public void SetTemporaryCooldown(int frames) =>
        _remainingFrames = Math.Max(0, frames);

    public void Reset() =>
        _remainingFrames = _cooldown;

    public void Ready() =>
        _remainingFrames = 0;

    public bool IsReady() =>
        IsActive && _remainingFrames <= 0;
}
