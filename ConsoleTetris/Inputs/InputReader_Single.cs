namespace ConsoleTetris.Inputs;

internal static class InputReader_Single
{
    public static Input ReadInput()
    {
        var key = ConsoleKey.None;
        while (Console.KeyAvailable)
        {
            key = Console.ReadKey(true).Key;
        }
        return KeyMapper.Map(key);
    }
}
