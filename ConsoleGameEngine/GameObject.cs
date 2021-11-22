namespace ConsoleGameEngine;

public abstract class GameObject
{
    public Guid Guid { get; }
    public ConsoleGame Game { get; }
    public GameObject(ConsoleGame game)
    {
        Game = game;
        Guid = Guid.NewGuid();
    }

    public virtual void Start() { }
    public virtual void Update(float dt) { }
    public virtual void Draw() { }
}