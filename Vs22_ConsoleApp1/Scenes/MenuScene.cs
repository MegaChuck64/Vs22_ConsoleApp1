using ConsoleGameEngine;
using Vs22_ConsoleApp1.GameObjects;

namespace Vs22_ConsoleApp1.Scenes;

public class MenuScene : Scene
{
    public MenuScene(ConsoleGame game) : base(game)
    {
        Components.Add("Menu", new Menu(this));
        game.SceneManager.CurrentSscene = "Menu";
    }
}
