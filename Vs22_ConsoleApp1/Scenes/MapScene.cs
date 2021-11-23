using ConsoleGameEngine;
using Vs22_ConsoleApp1.GameObjects;

namespace Vs22_ConsoleApp1.Scenes;

public class MapScene : Scene
{
    public MapScene(ConsoleGame game) : base(game)
    {
        Components.Add("Map", new Map(this));
        Components.Add("Player", new Player(this));
    }
}