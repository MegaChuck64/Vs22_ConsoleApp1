
using ConsoleGameEngine;

namespace Vs22_ConsoleApp1.GameObjects;

public class Menu : GameObject
{
    ConsoleString titleString;
    ConsoleString mapSceneString;
    ConsoleString antSceneString;
    ConsoleString testSceneString;
    public Menu(Scene scene) : base(scene)
    {

        titleString = new ConsoleString
        {
            X = 9,
            Y = 3,
            Text = "Hollow World"
        };


        titleString.Color = new ConsoleColor[titleString.Text.Length];
        titleString.BGColor = new ConsoleColor[titleString.Text.Length];
        for (int i = 0; i < titleString.Text.Length; i++)
        {
            var rc = 0;// Scene.Game.Rand.Next(7, 16);
            var rb = 7; //Scene.Game.Rand.Next(7, 16);
            if (rc == rb) rc = 15;
            titleString.Color[i] = (ConsoleColor)rc;
            titleString.BGColor[i] = (ConsoleColor)rb;
        }

        mapSceneString = new ConsoleString()
        {
            X = 9,
            Y = 5,
            Text = "Map (Space)"
        };


        mapSceneString.Color = new ConsoleColor[mapSceneString.Text.Length];
        mapSceneString.BGColor = new ConsoleColor[mapSceneString.Text.Length];
        for (int i = 0; i < mapSceneString.Text.Length; i++)
        {
            var rc = 0;// Scene.Game.Rand.Next(7, 16);
            var rb = 7; //Scene.Game.Rand.Next(7, 16);
            if (rc == rb) rc = 15;
            mapSceneString.Color[i] = (ConsoleColor)rc;
            mapSceneString.BGColor[i] = (ConsoleColor)rb;
        }


        antSceneString = new ConsoleString()
        {
            X = 9,
            Y = 7,
            Text = "Ant (Enter)"
        };


        antSceneString.Color = new ConsoleColor[antSceneString.Text.Length];
        antSceneString.BGColor = new ConsoleColor[antSceneString.Text.Length];
        for (int i = 0; i < antSceneString.Text.Length; i++)
        {
            var rc = 0;// Scene.Game.Rand.Next(7, 16);
            var rb = 7; //Scene.Game.Rand.Next(7, 16);
            if (rc == rb) rc = 15;
            antSceneString.Color[i] = (ConsoleColor)rc;
            antSceneString.BGColor[i] = (ConsoleColor)rb;
        }

        testSceneString = new ConsoleString()
        {
            X = 9,
            Y = 9,
            Text = "Test (Esc)"
        };


        testSceneString.Color = new ConsoleColor[testSceneString.Text.Length];
        testSceneString.BGColor = new ConsoleColor[testSceneString.Text.Length];
        for (int i = 0; i < testSceneString.Text.Length; i++)
        {
            var rc = 0;// Scene.Game.Rand.Next(7, 16);
            var rb = 7; //Scene.Game.Rand.Next(7, 16);
            if (rc == rb) rc = 15;
            testSceneString.Color[i] = (ConsoleColor)rc;
            testSceneString.BGColor[i] = (ConsoleColor)rb;
        }
    }

    public override void Start()
    {
        FastConsole.Init(30, 30, "Consolas", 20, 20);
        
    }

    public override void Update(float dt)
    {
        if (Input.PressedKeys[ConsoleKey.Spacebar])
        {
            Scene.Game.SceneManager.ChangeScene("Map");
        }
        else if (Input.PressedKeys[ConsoleKey.Escape])
        {
            Scene.Game.SceneManager.ChangeScene("Test");
        }
        else if (Input.PressedKeys[ConsoleKey.Enter])
        {
            Scene.Game.SceneManager.ChangeScene("Ant");
        }
    }
    public override void Draw()
    {
        titleString.Draw();
        mapSceneString.Draw();
        antSceneString.Draw();
        testSceneString.Draw();
    }
}