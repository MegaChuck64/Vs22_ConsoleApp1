namespace ConsoleGameEngine;

public interface IComponent
{
    void Start();
    void Update(float dt);
    void Draw();
}

public class ComponentCollection<T> : IComponent where T : IComponent
{
    public Dictionary<string, T> Components = new ();
    public virtual void Start()
    {
        foreach (var comp in Components.Values)
        {
            comp.Start();
        }
    }
    public virtual void Update(float dt)
    {
        foreach (var comp in Components.Values)
        {
            comp.Update(dt);
        }
    }
    public virtual void Draw()
    {
        try
        {
            foreach (var comp in Components.Values)
            {
                comp.Draw();
            }
        }
        catch(Exception e)
        {

        }
    }

}