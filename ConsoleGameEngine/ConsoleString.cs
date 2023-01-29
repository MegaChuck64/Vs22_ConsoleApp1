
namespace ConsoleGameEngine;

public struct ConsoleString
{
    public string Text;
    public ConsoleColor[] Color;
    public ConsoleColor[] BGColor;

    public float X;
    public float Y;
    
    public ConsoleString()
    {
        Text = String.Empty;
        Color = new ConsoleColor[Text.Length];
        BGColor = new ConsoleColor[Text.Length];
        X = 0;
        Y = 0;
    }

    public void Draw()
    {
        for (int i = 0; i < Text.Length; i++)
        { 
            FastConsole.WriteToBuffer(
                Math.Clamp((int)X + i, 0, 255), 
                Math.Clamp((int)Y, 0, 255), 
                Text[i], Color[i], BGColor[i]);
        }
        //we never want to try and draw at a negative point, use clamp
    }
}