namespace ConsoleTetris.Control;

internal class ScoreManager
{
    public int GetClearScore(int level, int clearedLinesCount)
    {
        // TODO: Implement proper score calculation.
        return level * clearedLinesCount;
    }

    public int GetSoftDropScore(int level)
    {
        return level;
    }

    public int GetHardDropScore(int level, int height)
    {
        return 2 * level * height;
    }
}
