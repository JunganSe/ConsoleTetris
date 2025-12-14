namespace ConsoleTetris.Rendering;

internal class Renderer
{
    public void DrawRectangle(int x, int y, int width, int height, bool doubleWall = false)
    {
        string parts = doubleWall ? "═║╔╗╚╝" : "─│┌┐└┘";
        string horizontalLine = new string(parts[0], width - 2); // Reusable horizontal line without corners.

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
}
