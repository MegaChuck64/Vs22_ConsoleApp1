using ConsoleGameEngine;
namespace Vs22_ConsoleApp1.GameObjects;
public class Map : GameObject
{
    public ConsoleChar[,] charMap;
    public Map(Scene scene) : base(scene)
    {
        var noiseMap = NoiseMap.Generate(scene.Game.Width, scene.Game.Height, 0.1f);
        charMap = new ConsoleChar[scene.Game.Width, scene.Game.Height];
        for (int x = 0; x < scene.Game.Width; x++)
        {
            for (int y = 0; y < scene.Game.Height; y++)
            { 
                var dist = DistFromCenter(x, y);
                var gradient = RemapRange(dist, 0f, 1f, 1f, 0f);
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
        var center = (Scene.Game.Width / 2, Scene.Game.Height / 2);
        float distance = (float)GetDistance(x, y, center.Item1, center.Item2);            
        return distance / Scene.Game.Width;
    }
    private static double GetDistance(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
    }

    public static float RemapRange(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }


}