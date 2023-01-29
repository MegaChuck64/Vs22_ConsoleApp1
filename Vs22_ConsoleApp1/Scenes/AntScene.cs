using ConsoleGameEngine;
using Vs22_ConsoleApp1.GameObjects;


namespace Vs22_ConsoleApp1.Scenes
{
    public class AntScene : Scene 
    {
        public AntScene(ConsoleGame game) : base(game)
        {
            Components.Add("Ant", new Ant(this));
        }
    }
}