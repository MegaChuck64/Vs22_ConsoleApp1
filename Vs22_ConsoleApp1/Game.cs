using ConsoleGameEngine;
using Vs22_ConsoleApp1.Scenes;

namespace Vs22_ConsoleApp1;
public class Game : ConsoleGame
{
    public Game(int w, int h) : base(w, h)
    {
        SceneManager.Components.Add("Test", new TestScene(this));
        SceneManager.Components.Add("Map", new MapScene(this));
    }


    public override void Start()
    {
        SceneManager.CurrentSscene = "Test";
    }

    public override void Update(float delta)
    {
        if (Input.PressedKeys[Input.Keys.Esc] && Input.PressedKeys[Input.Keys.End])
            State = GameState.Exiting;

    }

    public override void Draw()
    {

    }

}

