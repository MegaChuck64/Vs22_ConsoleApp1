
namespace ConsoleGameEngine;

public struct ConsoleObject
{
    public float X;
    public float Y;
    public ConsoleChar[,] map;

    public ConsoleObject(int x, int y, int w, int h, char c = ' ', ConsoleColor col = ConsoleColor.Black, ConsoleColor bgColor = ConsoleColor.Black)
    {
        X= x;
        Y = y;
        map = new ConsoleChar[w, h];
        for (int xx = 0; xx < w; xx++)
        {
            for (int yy = 0; yy < h; yy++)
            {
                map[xx, yy] = new ConsoleChar
                {
                    X = xx,
                    Y = yy,
                    C = c,
                    Color = col,
                    BGColor = bgColor
                };
            }
        }
    }


    public void Draw()
    {
        try
        {
            //we never want to try and draw at a negative point, use clamp
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y].X = x + X;
                    map[x, y].Y = y + Y;
                    map[x, y].Draw();
                    map[x, y].X = x;
                    map[x, y].Y = y;
                }
            }
        }
        catch (Exception e)
        {

        }
    }


}