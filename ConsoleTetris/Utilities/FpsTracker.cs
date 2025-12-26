namespace ConsoleTetris.Utilities;

internal class FpsTracker
{
    private const double _fpsUpdateInterval = 500;
    private double _fpsUpdateTimer = 0;
    private int _frameCount = 0;

    public double CurrentFps { get; private set; } = 0;

    /// <summary> Updates the FPS tracking. Should be called once per frame. </summary>
    /// <param name="deltaTime">The elapsed time (ms) since the previous frame.</param>
    public void Update(double deltaTime)
    {
        _fpsUpdateTimer += deltaTime;
        _frameCount++;

        if (_fpsUpdateTimer < _fpsUpdateInterval)
            return;

        CurrentFps = _frameCount * 1000.0 / _fpsUpdateTimer;

        _fpsUpdateTimer = 0;
        _frameCount = 0;
    }
}