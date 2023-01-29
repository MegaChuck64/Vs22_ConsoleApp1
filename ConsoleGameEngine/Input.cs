namespace ConsoleGameEngine;

//we use this input class using win32 key events because
//default console.readkey is not consistent. delay between first and second fire
//like in notepad.exe or similar prgrams 
public static class Input
{
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern short GetAsyncKeyState(int key);

    public static Dictionary<ConsoleKey, bool> PressedKeys = new();

    public static void Init()
    {
        foreach (var key in Enum.GetValues<ConsoleKey>())
        {
            PressedKeys.Add(key, false);
        }
    }
    public static void Update()
    {
        foreach (var key in PressedKeys.Keys)
        {            
            //0x8000 most significant bit
            //if MSB is set, key is down
            PressedKeys[key] = (0x8000 & GetAsyncKeyState((char)key)) != 0;
        }
    }

    ////https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
    //public enum Keys : short
    //{
    //    Left = 0x25,
    //    Up = 0x26,
    //    Right = 0x27,
    //    Down = 0x28,
    //    Esc = 0x1B,
    //    End = 0x23,
    //    Space = 0x20,
    //    Enter = 0x0D
    //}
}

