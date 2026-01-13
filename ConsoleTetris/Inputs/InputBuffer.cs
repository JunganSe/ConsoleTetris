namespace ConsoleTetris.Inputs;

internal class InputBuffer
{
    private int _bufferLimit;
    private readonly Dictionary<Input, int> _inputs = [];

    public InputBuffer(int bufferLimit)
    {
        _bufferLimit = bufferLimit;
    }

    /// <remarks> Call once per frame. </remarks>
    public void Update()
    {
        for (int i = _inputs.Count - 1; i >= 0; i--)
        {
            var input = _inputs.Keys.ElementAt(i);
            if (_inputs[input] < _bufferLimit)
                _inputs[input]++;
            else
                _inputs.Remove(input);
        }
    }

    /// <summary>
    /// Stores an input in the buffer. <br/>
    /// If the input is already buffered, its timer is reset.
    /// </summary>
    public void Buffer(Input input)
    {
        _inputs[input] = 0;
    }

    /// <summary>
    /// Checks if the input is buffered. <br/>
    /// If it is, releases the buffer and returns true.
    /// </summary>
    public bool TryRelease(Input input)
    {
        if (!_inputs.ContainsKey(input))
            return false;

        _inputs.Remove(input);
        return true;
    }
}
