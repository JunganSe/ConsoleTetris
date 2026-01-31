namespace ConsoleTetris.Mechanics;

// TODO: Count down instead of up.
internal class Cooldown
{
    private int _cooldown;
    private int _elapsedFrames;

    public bool IsActive { get; set; } = true;

    /// <remarks> Call once per frame. </remarks>
    public void Update()
    {
        if (IsActive && _elapsedFrames < _cooldown)
            _elapsedFrames++;
    }

    public void SetCooldown(int frames)
    {
        _cooldown = frames;
    }

    public bool IsReady()
    {
        return (IsActive && _elapsedFrames >= _cooldown);
    }

    public void Reset()
    {
        _elapsedFrames = 0;
    }
}
