namespace ConsoleGameEngine;

public abstract class Scene : ComponentCollection<GameObject>
{
    public ConsoleGame Game { get; }
    public Scene(ConsoleGame game)
    {
        Game = game;
    }
}