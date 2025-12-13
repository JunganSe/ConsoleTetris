namespace ConsoleTetris.Inputs;

public class InputReader_Multiple
{
    public static HashSet<Input> ReadInput()
    {
        var keys = new HashSet<ConsoleKey>();
        while (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            keys.Add(key);
        }

        var inputs = keys
            .Select(MapKey)
            .Where(input => input != Input.Nothing)
            .ToHashSet();
        return (inputs.Count > 0)
            ? inputs
            : [Input.Nothing];
    }

    private static Input MapKey(ConsoleKey key)
    {
        return key switch
        {
            ConsoleKey.LeftArrow => Input.Left,
            ConsoleKey.RightArrow => Input.Right,
            ConsoleKey.UpArrow => Input.Up,
            ConsoleKey.DownArrow => Input.Down,
            ConsoleKey.Q => Input.Quit,
            ConsoleKey.Spacebar => Input.Pause,
            _ => Input.Nothing,
        };
    }
}
