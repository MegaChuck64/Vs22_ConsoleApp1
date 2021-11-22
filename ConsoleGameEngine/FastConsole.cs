using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;


namespace ConsoleGameEngine;

public static class FastConsole
{

    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    static extern SafeFileHandle CreateFile(
        string fileName,
        [MarshalAs(UnmanagedType.U4)] uint fileAccess,
        [MarshalAs(UnmanagedType.U4)] uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] int flags,
        IntPtr template);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteConsoleOutputW(
      SafeFileHandle hConsoleOutput,
      CharInfo[] lpBuffer,
      Coord dwBufferSize,
      Coord dwBufferCoord,
      ref SmallRect lpWriteRegion);

    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
        public short X;
        public short Y;

        public Coord(short X, short Y)
        {
            this.X = X;
            this.Y = Y;
        }
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct CharUnion
    {
        [FieldOffset(0)] public ushort UnicodeChar;
        [FieldOffset(0)] public byte AsciiChar;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
        [FieldOffset(0)] public CharUnion Char;
        [FieldOffset(2)] public short Attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }


    private static SafeFileHandle bufferFile;
    private static CharInfo[] charBuffer;
    private static SmallRect drawRect;
    public static void Init(int w, int h)
    {
        bufferFile = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
        charBuffer = new CharInfo[w * h];
        for (int i = 0; i < charBuffer.Length; i++)
        {
            charBuffer[i] = new CharInfo()
            {
                Attributes = (short)ConsoleColor.DarkBlue,
                Char = new CharUnion() { AsciiChar = (byte)'g' }
            };
        }
        drawRect = new SmallRect { Left = 0, Top = 0, Right = (short)w, Bottom = (short)h };
    }

    public static void WriteToBuffer(int x, int y, char c, ConsoleColor color)
    {
        try
        {
            var i = y * drawRect.Right + x;
            charBuffer[i].Attributes = (short)color;
            charBuffer[i].Char.AsciiChar = (byte)c;
        }
        catch { }
    }
    public static void Draw()
    {
        if (!bufferFile.IsInvalid)
        {

            var tempRec = drawRect;

            bool goodDraw = WriteConsoleOutputW(bufferFile, charBuffer,
                new Coord() { X = drawRect.Right, Y = drawRect.Bottom },
                new Coord() { X = 0, Y = 0 },
                ref tempRec);
            if (!goodDraw)
            {
                var lastError = Marshal.GetLastWin32Error();
                System.Diagnostics.Debug.WriteLine($"Error drawing fast console output: \t{lastError}");
            }

        }
    }

    /// <summary>
    /// GRAPHICS TEST (prints grid of A - Z in every console color on keypress)
    /// </summary>
    public static void Test()
    {
        SafeFileHandle h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

        if (!h.IsInvalid)
        {
            CharInfo[] buf = new CharInfo[80 * 25];
            SmallRect rect = new SmallRect() { Left = 0, Top = 0, Right = 80, Bottom = 25 };

            for (byte character = 65; character < 65 + 26; ++character)
            {
                for (short attribute = 0; attribute < 15; ++attribute)
                {
                    for (int i = 0; i < buf.Length; ++i)
                    {
                        buf[i].Attributes = attribute;
                        buf[i].Char.AsciiChar = character;
                    }

                    bool b = WriteConsoleOutputW(h, buf,
                      new Coord() { X = 80, Y = 25 },
                      new Coord() { X = 0, Y = 0 },
                      ref rect);
                    Console.ReadKey();

                }
            }
        }
        Console.ReadKey();
    }
}