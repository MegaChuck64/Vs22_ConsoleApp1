using ConsoleGameEngine;
using System.Drawing;

namespace Vs22_ConsoleApp1.GameObjects
{
    public class SpriteTest : GameObject
    {
        public ConsoleObject sprite;
        public float speed = 10f;
        public SpriteTest(Scene scene, string fileName) : base(scene)
        {
            var fl = new Bitmap(@$"assets\{fileName}");
            sprite = new ConsoleObject(0, 0, fl.Width, fl.Height);
            for (int x = 0; x < fl.Width; x++)
            {
                for (int y = 0; y < fl.Height; y++)
                {
                    sprite.map[x, y].C = '█';
                    var cl = fl.GetPixel(x, y);
                    sprite.map[x, y].Color = ClosestConsoleColor(cl.R, cl.G, cl.B);
                }
            }
        }

        public override void Update(float dt)
        {
            var keys = Input.PressedKeys;
            var (X, Y) = (sprite.X, sprite.Y);

            if (keys[ConsoleKey.LeftArrow])
                if (X > 0)
                    X -= speed * dt;
            if (keys[ConsoleKey.RightArrow])
                if (X < Scene.Game.Width - 1)
                    X += speed * dt;
            if (keys[ConsoleKey.UpArrow])
                if (Y > 0)
                    Y -= speed * dt;
            if (keys[ConsoleKey.DownArrow])
                if (Y < Scene.Game.Height - 1)
                    Y += speed * dt;

            if (X > Scene.Game.Width - 1) X = Scene.Game.Width - 1;
            if (Y > Scene.Game.Height - 1) Y = Scene.Game.Height - 1;

            sprite.X = X;
            sprite.Y = Y;
        }
        public override void Draw()
        {
            sprite.Draw();
        }
        public ConsoleColor ClosestConsoleColor(byte r, byte g, byte b)
        {
            ConsoleColor ret = 0;
            double rr = r, gg = g, bb = b, delta = double.MaxValue;

            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                var n = Enum.GetName(typeof(ConsoleColor), cc);
                if (n == null) continue;
                var c = Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
                var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0);
                if (t == 0.0)
                    return cc;
                if (t < delta)
                {
                    delta = t;
                    ret = cc;
                }
            }
            return ret;
        }


    }
}
