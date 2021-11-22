using ConsoleGameEngine;
namespace Vs22_ConsoleApp1.GameObjects;
public class Map : GameObject
{
    public ConsoleChar[,] charMap;
    public Map(Game game) : base(game)
    {
        var noiseMap = NoiseMap.Generate(game.Width, game.Height, 0.1f);
        charMap = new ConsoleChar[game.Width, game.Height];
        for (int x = 0; x < game.Width; x++)
        {
            for (int y = 0; y < game.Height; y++)
            {
                var dist = DistFromCenter(x, y);
                var gradient = Remap(dist, 0f, 1f, 1f, 0f);
                var height = noiseMap[x, y] * gradient;

                (char c, ConsoleColor col) = height switch
                {
                    _ when height > 220 => ('▓', ConsoleColor.Gray),
                    _ when height > 160 => ('▒', ConsoleColor.DarkGray),
                    _ when height > 120 => ('░', ConsoleColor.Yellow),
                    _ when height > 80  => ('╬', ConsoleColor.DarkCyan),
                    _ when height > 40  => ('║', ConsoleColor.Blue),
                    _                   => ('╋', ConsoleColor.DarkBlue),
                };

                var randCol = game.Rand.Next(1, 16);
                charMap[x, y] = new ConsoleChar
                {
                    C = c,
                    Color = col,
                    X = x,
                    Y = y
                };
            }
        }
    }
    public override void Draw()
    {
        for (int x = 0; x < charMap.GetLength(0); x++)
        {

            for (int y = 0; y < charMap.GetLength(1); y++)
            {
                charMap[x, y].Draw();
            }
        }
    }

    private float DistFromCenter(float x, float y)
    {
        var center = (Game.Width / 2, Game.Height / 2);
        float distance = MathF.Sqrt(
            (center.Item1 - x) * (center.Item1 - x) + (center.Item2 - y) * (center.Item2 - y));
        return distance / Game.Width;
    }
    private static double GetDistance(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
    }

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }


}