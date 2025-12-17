using System.Runtime.InteropServices;

namespace ConsoleTetris.Inputs;

internal partial class InputReader
{
    [LibraryImport("user32.dll")]
    private static partial short GetAsyncKeyState(int vKey);

    public static bool IsKeyHeld(int vKey)
    {
        short state = GetAsyncKeyState(vKey);
        return (state & 0x8000) != 0;
    }
}
