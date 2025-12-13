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
            .Select(KeyMapper.Map)
            .Where(input => input != Input.Nothing)
            .ToHashSet();
        return (inputs.Count > 0)
            ? inputs
            : [Input.Nothing];
    }
}
