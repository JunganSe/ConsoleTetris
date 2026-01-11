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

    public void DrawHeldTetromino(TetrominoShape? shape)
    {
        Console.ForegroundColor = (shape.HasValue)
            ? ColorMapper.GetTetrominoColor(shape.Value)
            : ConsoleColor.White;
        var relativeCoords = (shape.HasValue)
            ? Tetromino.GetRelativePiecesCoords(shape.Value, Direction.A)
            : [];

        for (int x = 0; x < GuiPosition.HoldWidth; x++)
        {
            for (int y = 0; y < GuiPosition.HoldHeight; y++)
            {
                int cursorX = GuiPosition.HoldX + x * 2;
                int cursorY = GuiPosition.HoldY - y;
                string texture = relativeCoords.Any(coord => coord.X == x && coord.Y == y)
                    ? PieceTexture.Block
                    : PieceTexture.Empty;
                Console.SetCursorPosition(cursorX, cursorY);
                Console.Write(texture);
            }
        }
    }

    public void DrawNextTetromino(TetrominoShape shape)
    {
        Console.ForegroundColor = ColorMapper.GetTetrominoColor(shape);
        var relativeCoords = Tetromino.GetRelativePiecesCoords(shape, Direction.A);

        for (int x = 0; x < GuiPosition.NextWidth; x++)
        {
            for (int y = 0; y < GuiPosition.NextHeight; y++)
            {
                int cursorX = GuiPosition.NextX + x * 2;
                int cursorY = GuiPosition.NextY - y;
                string texture = relativeCoords.Any(coord => coord.X == x && coord.Y == y)
                    ? PieceTexture.Block
                    : PieceTexture.Empty;
                Console.SetCursorPosition(cursorX, cursorY);
                Console.Write(texture);
            }
        }
    }

    public void DrawFps(double fps)
    {
        string fpsText = string.Format($"{fps:F0}").PadLeft(2); // F0 formats as integer.
        Console.ForegroundColor = GuiColor.FpsText;
        Console.SetCursorPosition(GuiPosition.FpsX, GuiPosition.FpsY);
        Console.Write($"FPS:{fpsText}");
    }
}
