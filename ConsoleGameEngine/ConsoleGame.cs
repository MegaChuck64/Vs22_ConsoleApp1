namespace ConsoleGameEngine;

public abstract class ConsoleGame
{
    public int Width { get; }
    public int Height { get; }

    public List<GameObject> GameObjects = new();
    public GameState State { get; set; }
    public Random Rand { get; set; }

    public ConsoleGame(int w, int h)
    {
        Width = w;
        Height = h;
        Console.SetWindowSize(w, h);
        Console.SetBufferSize(w, h);
        Console.CursorVisible = false;

        Rand = new Random();
    }

    public void Run(float fps)
    {
        if (State != GameState.Starting) return;


        FastConsole.Init(Width, Height);
        Input.Init();
        Start();
        DateTime lastGameTime = DateTime.Now;
        State = GameState.Running;
        while (State == GameState.Running)
        {

            TimeSpan GameTime = DateTime.Now - lastGameTime;

            lastGameTime += GameTime;

            //draw last frame first 
            FastConsole.Draw();

            Input.Update();

            //dt (delta time), time between frames. Allows movement by units per second instead of per frame
            Update((float)(GameTime.TotalMilliseconds / 1000));

            //draws to fast console buffer
            Draw();

            //constant frame rate, still calculating dt above, because thread sleep aint perfect 
            Thread.Sleep((int)(1000 / fps));
        }
    }


    public virtual void Start()
    {
        foreach (var go in GameObjects)
        {
            go.Start();
        }
    }
    public virtual void Update(float delta)
    {
        foreach (var go in GameObjects)
        {
            go.Update(delta);
        }
    }
    public virtual void Draw()
    {
        foreach (var go in GameObjects)
        {
            go.Draw();
        }
    }

    public enum GameState
    {
        Starting,
        Running,
        Exiting
    }
}

