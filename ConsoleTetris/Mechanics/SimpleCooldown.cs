namespace ConsoleTetris.Mechanics;

internal class SimpleCooldown
{
    private int _cooldown;
    private int _elapsedFrames;

    /// <remarks> Call once per frame. </remarks>
    public void Update()
    {
        if (_elapsedFrames < _cooldown)
            _elapsedFrames++;
    }

    public void SetCooldown(int frames)
    {
        _cooldown = frames;
    }

    public bool IsReady()
    {
        return _elapsedFrames >= _cooldown;
    }

    public void Reset()
    {
        _elapsedFrames = 0;
    }
}
