namespace ConsoleTetris.Utilities;

internal class FpsTracker
{
    private double _fpsUpdateTimer = 0;
    private const double _fpsUpdateInterval = 200;

    public double CurrentFps { get; private set; } = 0;

    /// <summary> Updates the FPS tracking. Should be called once per frame. </summary>
    /// <param name="deltaTime">The elapsed time (ms) since the previous frame.</param>
    public void Update(double deltaTime)
    {
        _fpsUpdateTimer += deltaTime;
        if (_fpsUpdateTimer < _fpsUpdateInterval)
            return;

        _fpsUpdateTimer = 0;
        CurrentFps = (deltaTime > 0)
            ? 1000.0 / deltaTime
            : 0;
    }
}
