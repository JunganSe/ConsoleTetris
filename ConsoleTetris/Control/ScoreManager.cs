namespace ConsoleTetris.Control;

internal class ScoreManager
{
    public int GetClearScore(int level, int clearedLinesCount)
    {
        // TODO: Implement proper score calculation.
        return level * clearedLinesCount;
    }
}
