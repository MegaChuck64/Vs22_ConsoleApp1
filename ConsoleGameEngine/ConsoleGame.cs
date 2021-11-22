namespace ConsoleGameEngine;

public abstract class ConsoleGame
{
    public int Width { get; }
    public int Height { get; }

    public List<GameObject> GameObjects = new();
    public ConsoleGame(int w, int h)
    {
        Width = w;
        Height = h;
        Console.SetWindowSize(w, h);
        Console.SetBufferSize(w + 1, h + 1);
        Console.CursorVisible = false;
    }
    public void Start()
    {
        foreach (var go in GameObjects)
        {
            go.Start();
        }
    }
    public void Update(float delta)
    {
        foreach (var go in GameObjects)
        {
            go.Update(delta);
        }
    }
    public void Draw()
    {
        foreach (var go in GameObjects)
        {
            go.Draw();
        }
    }
}

