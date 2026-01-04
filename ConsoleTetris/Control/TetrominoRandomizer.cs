using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Control;

internal class TetrominoRandomizer
{
    private readonly Random _random = new();
    private readonly Stack<TetrominoShape> _bag = new();
    private readonly TetrominoShape[] _possibleShapes = Enum.GetValues<TetrominoShape>();

    public TetrominoShape GetNext()
    {
        if (_bag.Count == 0)
            FillBag();

        return _bag.Pop();
    }

    public TetrominoShape PeekNext()
    {
        if (_bag.Count == 0)
            FillBag();

        return _bag.Peek();
    }

    private void FillBag()
    {
        var shapes = _possibleShapes.OrderBy(_ => _random.Next()).ToArray();
        foreach (var shape in shapes)
            _bag.Push(shape);
    }
}
