using System.Runtime.InteropServices;

namespace ConsoleTetris.Inputs;

internal partial class InputReader
{
    // Gets a set of all keys in the provided integer enum that are currently being held down.
    public static HashSet<TKey> GetHeldKeys<TKey>() where TKey : struct, Enum
    {
        return Enum.GetValues<TKey>()
            .Where(key => IsKeyHeld(Convert.ToInt32(key)))
            .ToHashSet();
    }



    // Checks if a specific key is currently being held down.
    private static bool IsKeyHeld(int vKey)
    {
        short state = GetAsyncKeyState(vKey);
        return (state & 0x8000) != 0;
    }

    [LibraryImport("user32.dll")]
    private static partial short GetAsyncKeyState(int vKey);
}
