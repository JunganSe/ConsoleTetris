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

    // Checks if a specific key was pressed (and still is) since the last call to GetAsyncKeyState.
    public static bool IsKeyPressed(Key key)
    {
        short state = GetAsyncKeyState((int)key);
        return (state & 0x8001) != 0;
    }

    // Gets a set of all keys in the "Key" enum that are currently being held down.
    public static HashSet<Key> GetHeldKeys()
    {
        var heldKeys = new HashSet<Key>();

        foreach (Key key in Enum.GetValues<Key>())
        {
            if (IsKeyHeld((int)key))
                heldKeys.Add(key);
        }

        return heldKeys;
    }
}
