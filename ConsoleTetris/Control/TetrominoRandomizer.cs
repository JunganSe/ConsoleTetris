using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Control;

internal class TetrominoRandomizer
{
    private readonly Random _random = new();
    private readonly Stack<TetrominoShape> _bag = new();
    private readonly TetrominoShape[] _possibleShapes = Enum.GetValues<TetrominoShape>();

    public TetrominoShape GetNext()
    {
        FillBagIfEmpty();
        return _bag.Pop();
    }

    public TetrominoShape PeekNext()
    {
        FillBagIfEmpty();
        return _bag.Peek();
    }

    private void FillBagIfEmpty()
    {
        if (_bag.Count > 0)
            return;

        var shapes = _possibleShapes.OrderBy(_ => _random.Next()).ToArray();
        foreach (var shape in shapes)
            _bag.Push(shape);
    }
}
