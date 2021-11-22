using ConsoleGameEngine;
using Vs22_ConsoleApp1.GameObjects;

namespace Vs22_ConsoleApp1;
public class Game : ConsoleGame
{
    public Game(int w, int h) : base(w, h)
    {
        GameObjects.Add(new Map(this));
        GameObjects.Add(new Player(this));
    }

    public override void Update(float delta)
    {
        if (Input.PressedKeys[Input.Keys.Esc] && Input.PressedKeys[Input.Keys.End])
            State = GameState.Exiting;

        base.Update(delta);
    }
}

