namespace ConsoleGameEngine;

public struct ConsoleChar
{
    public char C;
    public float X;
    public float Y;
    public ConsoleColor Color;
    public ConsoleColor BGColor;


    public void Draw()
    {
        try
        {
            //we never want to try and draw at a negative point, use clamp
            FastConsole.WriteToBuffer(Math.Clamp((int)X, 0, 255), Math.Clamp((int)Y, 0, 255), C, Color, BGColor);
        }
        catch(Exception e)
        {

        }
    }
}