using System.Runtime.InteropServices;

namespace ConsoleTetris.Inputs;

internal partial class InputReader
{
    [LibraryImport("user32.dll")]
    private static partial short GetAsyncKeyState(int vKey);

    // Checks if a specific key is currently being held down.
    public static bool IsKeyHeld(int vKey)
    {
        short state = GetAsyncKeyState(vKey);
        return (state & 0x8000) != 0;
    }

    // Gets a set of all keys in the provided enum that are currently being held down.
    public static HashSet<TKey> GetHeldKeys<TKey>() where TKey : struct, Enum
    {
        var heldKeys = new HashSet<TKey>();
        foreach (TKey key in Enum.GetValues<TKey>())
        {
            if (IsKeyHeld(Convert.ToInt32(key)))
                heldKeys.Add(key);
        }
        return heldKeys;
    }
}
