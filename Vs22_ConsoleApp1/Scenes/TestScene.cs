using ConsoleGameEngine;
using Vs22_ConsoleApp1.GameObjects;

namespace Vs22_ConsoleApp1.Scenes;

public class TestScene : Scene
{
    public TestScene(ConsoleGame game) : base(game)
    {
        Components.Add("Sprite", new SpriteTest(this, "TestSprite.png"));
    }
}