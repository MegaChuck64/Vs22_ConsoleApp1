using Vs22_ConsoleApp1;
using ConsoleGameEngine;

var w = 64;
var h = 32;
var fps = 16f;

var game = new Game(w, h);

FastConsole.Init(w, h);
Input.Init();
game.Start();

DateTime lastGameTime = DateTime.Now;

while (true)
{

    TimeSpan GameTime = DateTime.Now - lastGameTime;

    lastGameTime += GameTime;

    //draw last frame first 
    FastConsole.Draw();

    Input.Update();

    //dt (delta time), time between frames. Allows movement by units per second instead of per frame
    game.Update((float)(GameTime.TotalMilliseconds / 1000));
       
    //draws to fast console buffer
    game.Draw();

    //constant frame rate, still calculating dt above, because thread sleep aint perfect 
    Thread.Sleep((int)(1000 / fps));
}