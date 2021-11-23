namespace ConsoleGameEngine;

public class SceneManager : ComponentCollection<Scene>
{
    public ConsoleGame Game { get; }

    public string CurrentSscene = string.Empty;
    public SceneManager(ConsoleGame game)
    {
        Game = game;
    }

    public override void Start()
    {
        Components[CurrentSscene].Start();
    }

    public override void Update(float dt)
    {
        Components[CurrentSscene].Update(dt);
    }

    public override void Draw()
    {
        Components[CurrentSscene].Draw();
    }

}