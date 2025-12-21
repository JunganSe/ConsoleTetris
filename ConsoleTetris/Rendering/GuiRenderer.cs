namespace ConsoleTetris.Rendering;

internal class GuiRenderer
{
    private const string _singleWallParts = "─│┌┐└┘";
    private const string _doubleWallParts = "═║╔╗╚╝";

    public void DrawPlayAreaBorder() =>
        DrawRectangle(
            GlobalConstants.PlayArea.X * 2 - 1,
            GlobalConstants.PlayArea.Y - 1,
            GlobalConstants.PlayArea.Width * 2 + 2,
            GlobalConstants.PlayArea.Height + 2,
            _doubleWallParts);

    /// <param name="parts">A string containing the line and corner parts in this order: "─│┌┐└┘"</param>
    private void DrawRectangle(int x, int y, int width, int height, string parts)
    {
        string horizontalLine = new(parts[0], width - 2); // Reusable horizontal line without corners.

        // Top part
        Console.SetCursorPosition(x, y);
        Console.Write(parts[2] + horizontalLine + parts[3]);

        // Middle part (sides).
        for (int i = 1; i < height - 1; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.Write(parts[1]);
            Console.SetCursorPosition(x + width - 1, y + i);
            Console.Write(parts[1]);
        }

        // Bottom part.
        Console.SetCursorPosition(x, y + height - 1);
        Console.Write(parts[4] + horizontalLine + parts[5]);
    }

    public void DrawFps(double fps)
    {
        Console.SetCursorPosition(Console.WindowWidth - 7, 0);
        Console.Write($"FPS: {fps:F0}"); // F0 formats as integer.
    }
}
