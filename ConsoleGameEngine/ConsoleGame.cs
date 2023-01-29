namespace ConsoleGameEngine;

public abstract class ConsoleGame : IComponent
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public GameState State { get; set; }
    public Random Rand { get; set; }

    public SceneManager SceneManager;


    public ConsoleGame(int w, int h)
    {
        Width = w;
        Height = h;
        Console.CursorVisible = false;
        SceneManager = new SceneManager(this);

        Rand = new Random();
    }

    public void Run(float fps, string font = "Consolas", int fw = 14, int fh = 14)
    {
        if (State != GameState.Starting) return;


        FastConsole.Init(Width, Height, font, fw, fh);
        Width = FastConsole.drawRect.Right;
        Height = FastConsole.drawRect.Bottom;
        Input.Init();
       
        Start();
        SceneManager.Start();

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
            var dt = (float)(GameTime.TotalMilliseconds / 1000);
            Update(dt);
            SceneManager.Update(dt);

            //draws to fast console buffer
            Draw();
            SceneManager.Draw();

            //constant frame rate, still calculating dt above, because thread sleep aint perfect 
            Thread.Sleep((int)(1000 / fps));
        }
    }

    public abstract void Start();
    public abstract void Update(float delta);
    public abstract void Draw();

    public enum GameState
    {
        Starting,
        Running,
        Exiting
    }
}

