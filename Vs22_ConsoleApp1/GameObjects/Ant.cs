using ConsoleGameEngine;

namespace Vs22_ConsoleApp1.GameObjects;

public class Ant : GameObject
{
    public List<Walker> walkers = new ();
    public ConsoleChar[,] map;
    public Ant(Scene scene) : base(scene)
    {
        map = new ConsoleChar[scene.Game.Width, scene.Game.Height];
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                map[x, y] = new ConsoleChar()
                {
                    C = '▓',
                    Color = ConsoleColor.Red,
                    X = x,
                    Y = y,
                    BGColor = ConsoleColor.Black
                };
            }
        }

        walkers.Add(new Walker { x = scene.Game.Width/2, y = scene.Game.Height/2, steps = 1000 });
    }

    float stepsPerSecond = 25f;
    float timer = 1f;
    public override void Update(float dt)
    {
        timer -= dt;
        if (timer <= 0f)
        {
            timer = 1f/stepsPerSecond;
            foreach (var walker in walkers)
            {
                if (walker.steps > 0)
                {
                    walker.Walk(ref map);
                }
            }
        }
    }

    public override void Draw()
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {

            for (int y = 0; y < map.GetLength(1); y++)
            {
                map[x, y].Draw();
            }
        }

        foreach (var walker in walkers)
        {
            FastConsole.WriteToBuffer(walker.x, walker.y, '@', ConsoleColor.Yellow);
        }
    }
}


public class Walker
{
    public int x;
    public int y;
    public int steps;
    public Direction direction;
    public void Walk(ref ConsoleChar[,] map)
    {
        steps--;
        if (map[x,y].Color == ConsoleColor.Red)
        {
            map[x, y].Color = ConsoleColor.Blue;
            direction = direction switch
            {
                Direction.up => Direction.right,
                Direction.right => Direction.down,
                Direction.down => Direction.left,
                Direction.left => Direction.up
            };
        }
        else
        {
            map[x, y].Color = ConsoleColor.Red;
            direction = direction switch
            {
                Direction.up => Direction.left,
                Direction.right => Direction.up,
                Direction.down => Direction.right,
                Direction.left => Direction.down
            };
        }

        switch (direction)
        {
            case Direction.up:
                if (y > 0) y--;
                break;
            case Direction.right:
                if (x < map.GetLength(0) - 1) x++;
                break;
            case Direction.down:
                if (y < map.GetLength(1) - 1) y++;
                break;
            case Direction.left:
                if (x > 0) x--;
                break;
        }

    }

    public enum Direction
    {
        up,
        right,
        down,
        left
    }
}

