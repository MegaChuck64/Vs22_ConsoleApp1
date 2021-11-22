namespace ConsoleGameEngine;

public struct ConsoleChar
{
    public char C;
    public float X;
    public float Y;
    public ConsoleColor Color;

    public void Draw()
    {
        FastConsole.WriteToBuffer(Math.Clamp((int)X, 0, 255), Math.Clamp((int)Y, 0, 255), C, Color);
    }
}