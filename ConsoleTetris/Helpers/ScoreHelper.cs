namespace ConsoleTetris.Helpers;

internal static class ScoreHelper
{
    public static int GetLineClearScore(int level, int clearedLinesCount)
    {
        int multiplier = clearedLinesCount switch
        {
            1 => 100 * level,
            2 => 300 * level,
            3 => 500 * level,
            4 => 800 * level,
            _ => 0,
        };
        return level * multiplier;
    }

    public static int GetSoftDropScore(int level)
    {
        return level;
    }

    public static int GetHardDropScore(int level, int height)
    {
        return 2 * level * height;
    }
}
