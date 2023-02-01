using ConsoleGameEngine;
namespace Vs22_ConsoleApp1.GameObjects;
public class Player : GameObject
{
    ConsoleChar C;
    readonly float speed = 10f;
    readonly Map map;
    public Player(Scene scene) : base(scene)
    {
        map = (Map)scene.Components["Map"];

        C.X = scene.Game.Width / 2;
        C.Y = scene.Game.Height / 2;
        C.Color = ConsoleColor.Red;        
        C.BGColor = map.charMap[(int)C.X, (int)C.Y].Color;
        C.C = '@';

    }
    //bool done = false;
    public override void Update(float dt)
    {
        var keys = Input.PressedKeys;
        var (X, Y) = (C.X, C.Y);

        if (keys[ConsoleKey.LeftArrow])
            if (X > 0)
                X -= speed * dt;
        if (keys[ConsoleKey.RightArrow])
            if (X < Scene.Game.Width - 1)
                X+= speed * dt;
        if (keys[ConsoleKey.UpArrow])
            if (Y > 0)
                Y-= speed * dt;
        if (keys[ConsoleKey.DownArrow])
            if (Y < Scene.Game.Height - 1)
                Y+= speed * dt;

        if (X > Scene.Game.Width - 1) X = Scene.Game.Width - 1;
        if (Y > Scene.Game.Height - 1) Y = Scene.Game.Height - 1;

        var waterChars = new char[] { '╬','║','╋'};
        if (!waterChars.Contains(map.charMap[(int)X,(int)Y].C))
        {
            C.X = X;
            C.Y = Y;
        }

        //if (!done && keys[ConsoleKey.C])
        //{            
        //    map.offsetX += (int)C.X;
        //    map.offsetY += (int)C.Y;

        //    map.UpdateMap();
        //    done = true;
        //}
    }

    public override void Draw()
    {
        C.BGColor = map.charMap[(int)C.X, (int)C.Y].Color;
        C.Draw();
    }
}