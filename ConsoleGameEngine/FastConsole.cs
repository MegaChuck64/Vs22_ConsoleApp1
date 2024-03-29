﻿using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;


namespace ConsoleGameEngine;

public static class FastConsole
{

    //https://www.pinvoke.net/default.aspx/kernel32.CreateFile
    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern SafeFileHandle CreateFile(
        string fileName,
        uint fileAccess,
        uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        int flags,
        IntPtr template);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool WriteConsoleOutputW(
      SafeFileHandle hConsoleOutput,
      CharInfo[] lpBuffer,
      Coord dwBufferSize,
      Coord dwBufferCoord,
      ref SmallRect lpWriteRegion);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern Int32 SetCurrentConsoleFontEx(
      IntPtr ConsoleOutput,
      bool MaximumWindow,
      ref CONSOLE_FONT_INFO_EX ConsoleCurrentFontEx);

    [DllImport("kernel32")]
    private static extern IntPtr GetStdHandle(StdHandle index);


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
        [FieldOffset(2)] public ushort Attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CONSOLE_FONT_INFO_EX
    {
        public uint cbSize;
        public uint nFont;
        public Coord dwFontSize;
        public int FontFamily;
        public int FontWeight;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] // Edit sizeconst if the font name is too big
        public string FaceName;
    }


    public struct FastConsoleColor
    {
        public byte r;
        public byte g;
        public byte b;
    }

    private enum StdHandle
    {
        OutputHandle = -11
    }


    //warnings, no constructor
    //todo: maybe this shouldn't be static
    private static SafeFileHandle bufferFile;
    private static CharInfo[] charBuffer;
    public static SmallRect drawRect;
    public static void Init(int w, int h, string font = "Consolas", int fw = 14, int fh = 14)
    {
        SetConsoleFont(font, (short)fw, (short)fh);
        try
        {
            Console.SetWindowSize(w, h);
            Console.SetBufferSize(w, h);
        }
        catch
        {
            var size = GetLargestSquareWindowSize();
            Console.SetWindowSize(size, size);
            Console.SetBufferSize(size, size);
            w = size;
            h = size;
        }

        bufferFile = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
        charBuffer = new CharInfo[w * h];
        for (int i = 0; i < charBuffer.Length; i++)
        {
            charBuffer[i] = new CharInfo()
            {
                Attributes = (ushort)ConsoleColor.DarkBlue,
                Char = new CharUnion() { AsciiChar = (byte)'?' }
            };
        }
        drawRect = new SmallRect { Left = 0, Top = 0, Right = (short)w, Bottom = (short)h };
        Console.OutputEncoding = System.Text.Encoding.Unicode;
    }

    public static void WriteToBuffer(int x, int y, char c, ConsoleColor color, ConsoleColor bgColor = ConsoleColor.Black)
    {
        try
        {
            var i = y * drawRect.Right + x;
            charBuffer[i].Attributes = (ushort)((ushort)color | ((ushort)bgColor << 4));
            charBuffer[i].Char.UnicodeChar = c;
        }
        catch(Exception e)
        {
        
        }
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


    public static void SetConsoleFont(string fontName = "Consolas", short w = 14, short h = 14)
    {
        CONSOLE_FONT_INFO_EX ConsoleFontInfo = new()
        {
            FaceName = fontName,
            dwFontSize = new Coord(w,h)
        };

        ConsoleFontInfo.cbSize = (uint)Marshal.SizeOf(ConsoleFontInfo);
        _ = SetCurrentConsoleFontEx(GetStdHandle(StdHandle.OutputHandle), false, ref ConsoleFontInfo);

        // GetCurrentConsoleFontEx(GetStdHandle(StdHandle.OutputHandle), false, ref ConsoleFontInfo);
    }

    public static int GetLargestSquareWindowSize()
    {
        var largestW = Console.LargestWindowWidth;
        var largestH = Console.LargestWindowHeight;

        if (largestW < largestH) return largestW;
        else return largestH;
    }

    public static ConsoleColor ClosestConsoleColor(byte r, byte g, byte b)
    {
        ConsoleColor ret = 0;
        double rr = r, gg = g, bb = b, delta = double.MaxValue;

        foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
        {
            var n = Enum.GetName(typeof(ConsoleColor), cc);
            var c = System.Drawing.Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
            var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0);
            if (t == 0.0)
                return cc;
            if (t < delta)
            {
                delta = t;
                ret = cc;
            }
        }
        return ret;
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
            SmallRect rect = new() { Left = 0, Top = 0, Right = 80, Bottom = 25 };

            for (byte character = 65; character < 65 + 26; ++character)
            {
                for (ushort attribute = 0; attribute < 15; ++attribute)
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