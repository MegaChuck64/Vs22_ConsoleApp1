using ConsoleGameEngine;
namespace Vs22_ConsoleApp1.GameObjects;
public class Map : GameObject
{
    public ConsoleChar[,] charMap;
    public Map(Game game) : base(game)
    {
        charMap = new ConsoleChar[game.Width, game.Height];
        for (int x = 0; x < game.Width; x++)
        {
            for (int y = 0; y < game.Height; y++)
            {
                charMap[x, y] = new ConsoleChar
                {
                    C = '%',
                    Color = ConsoleColor.Green,
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
}