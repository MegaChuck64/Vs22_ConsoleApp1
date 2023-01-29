using Vs22_ConsoleApp1;

var w = 30;
var h = 30;
var fps = 120f;

var game = new Game(w, h);

game.Run(fps, "Consolas", 12, 12);

//Console.WriteLine($"w: {Console.LargestWindowWidth}, h:{Console.LargestWindowHeight}");