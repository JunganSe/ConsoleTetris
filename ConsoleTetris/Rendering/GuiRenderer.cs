using ConsoleTetris.GameComponents;

namespace ConsoleTetris.Rendering;

internal class GuiRenderer
{
    public void DrawGui()
    {
        // Gui characters: ═ ║ ╔ ╗ ╚ ╝ ╦ ╩ ╠ ╣ ╬
        Console.ForegroundColor = GuiColor.Border;
        Console.SetCursorPosition(0, 0);
        Console.Write(
            """
            ╔════════════════════╦════════╗
            ║                    ║ SCORE  ║
            ║                    ║        ║
            ║                    ╠════════╣
            ║                    ║ HOLD   ║
            ║                    ║        ║
            ║                    ║        ║
            ║                    ╠════════╣
            ║                    ║ NEXT   ║
            ║                    ║        ║
            ║                    ║        ║
            ║                    ╠════════╝
            ║                    ║
            ║                    ║
            ║                    ║
            ║                    ║
            ║                    ║
            ║                    ║
            ║                    ║
            ║                    ║
            ║                    ║
            ╚════════════════════╝
            """);
    }

    public void DrawFps(double fps)
    {
        Console.ForegroundColor = GuiColor.FpsText;
        Console.SetCursorPosition(Console.WindowWidth - 7, 0);
        Console.Write($"FPS: {fps:F0}"); // F0 formats as integer.
    }
}
