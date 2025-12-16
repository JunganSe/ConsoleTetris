using System.Runtime.InteropServices;

namespace ConsoleTetris.Inputs;

internal partial class InputReader
{
    [LibraryImport("user32.dll")]
    private static partial short GetAsyncKeyState(int vKey);

    // Virtual key codes for common keys
    public const int VK_SPACE = 0x20;
    public const int VK_ESCAPE = 0x1B;
    public const int VK_RETURN = 0x0D;
    public const int VK_UP = 0x26;
    public const int VK_DOWN = 0x28;
    public const int VK_LEFT = 0x25;
    public const int VK_RIGHT = 0x27;
    public const int VK_W = 0x57;
    public const int VK_A = 0x41;
    public const int VK_S = 0x53;
    public const int VK_D = 0x44;

    public static bool IsKeyHeld(int vKey)
    {
        short state = GetAsyncKeyState(vKey);
        return (state & 0x8000) != 0;
    }
}
