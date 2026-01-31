namespace ConsoleTetris.Mechanics;

internal class Cooldown
{
    private int _cooldown;
    private int _remainingFrames;

    public bool IsActive { get; set; } = true;

    /// <remarks> Call once per frame. </remarks>
    public void Update()
    {
        if (IsActive && _remainingFrames > 0)
            _remainingFrames--;
    }

    public void SetCooldown(int frames)
    {
        _cooldown = frames;
    }

    public void SetTemporaryCooldown(int frames)
    {
        _remainingFrames = frames;
    }

    public void Ready()
    {
        _remainingFrames = 0;
    }

    public bool IsReady()
    {
        return (IsActive && _remainingFrames <= 0);
    }

    public void Reset()
    {
        _remainingFrames = _cooldown;
    }
}
