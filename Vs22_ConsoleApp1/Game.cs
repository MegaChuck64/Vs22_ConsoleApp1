using ConsoleGameEngine;
using Vs22_ConsoleApp1.Scenes;

namespace Vs22_ConsoleApp1;
public class Game : ConsoleGame
{
    public Game(int w, int h) : base(w, h)
    {
        SceneManager.Components.Add("Test", new TestScene(this));
        SceneManager.Components.Add("Ant", new AntScene(this));
        SceneManager.Components.Add("Map", new MapScene(this));
        SceneManager.Components.Add("Menu", new MenuScene(this));
    }


    public override void Start()
    {
        SceneManager.CurrentSscene = "Menu";
    }

    public override void Update(float delta)
    {
        if (Input.PressedKeys[ConsoleKey.Escape] && Input.PressedKeys[ConsoleKey.End])
            State = GameState.Exiting;

    }

    public override void Draw()
    {

    }

}

