namespace ConsoleTetris.Helpers;

internal static class GravityHelper
{
    public static int GetGravityCooldown(int level)
    {
        return level switch
        {
            <= 10 => 32 - level * 2,
            <= 19 => 21 - level,
            _ => 1,
        };
    }
}
