using ConsoleGameEngine;
namespace Vs22_ConsoleApp1.GameObjects;
public class Player : GameObject
{
    ConsoleChar C;
    float speed = 16f;
    public Player(Game game) : base(game)
    {
        C.X = game.Width / 2;
        C.Y = game.Height / 2;
        C.Color = ConsoleColor.Red;
        C.C = '@';
    }

    public override void Update(float dt)
    {
        var keys = Input.PressedKeys;

        if (keys[Input.Keys.Left])
            if (C.X > 0)
                C.X -= speed * dt;
        if (keys[Input.Keys.Right])
            if (C.X < Game.Width - 1)
                C.X+= speed * dt;
        if (keys[Input.Keys.Up])
            if (C.Y > 0)
                C.Y-= speed * dt;
        if (keys[Input.Keys.Down])
            if (C.Y < Game.Height - 1)
                C.Y+= speed * dt;

        if (C.X > Game.Width - 1) C.X = Game.Width - 1;
        if (C.Y > Game.Height - 1) C.Y = Game.Height - 1;
    }

    public override void Draw()
    {
        C.Draw();
    }
}