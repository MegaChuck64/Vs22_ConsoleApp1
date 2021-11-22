namespace ConsoleGameEngine;

public static class Input
{
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern short GetAsyncKeyState(int key);

    public static Dictionary<Keys, bool> PressedKeys = new();

    public static void Init()
    {
        foreach (var key in Enum.GetValues<Keys>())
        {
            PressedKeys.Add(key, false);
        }
    }
    public static void Update()
    {
        foreach (var key in PressedKeys.Keys)
        {
            //0x8000 = int minimum
            PressedKeys[key] = (0x8000 & GetAsyncKeyState((char)key)) != 0;
        }
    }

    //https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
    public enum Keys : short
    {
        Left = 0x25,
        Up = 0x26,
        Right = 0x27,
        Down = 0x28,
    }
}

