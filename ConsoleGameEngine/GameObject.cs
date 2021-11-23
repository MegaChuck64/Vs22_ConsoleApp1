namespace ConsoleGameEngine;

public abstract class GameObject : IComponent
{
    public Scene Scene { get; }
    public GameObject(Scene scene)
    {
        Scene = scene;
    }
    public virtual void Start() { }
    public virtual void Update(float dt) { }
    public virtual void Draw() { }
}