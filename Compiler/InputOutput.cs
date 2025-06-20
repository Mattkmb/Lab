using System;
using System.Collections.Generic;
using System.IO;

namespace Компилятор
{
    struct TextPosition
    {
        public uint lineNumber;
        public byte charNumber;

        public TextPosition(uint ln = 0, byte c = 0)
        {
            lineNumber = ln;
            charNumber = c;
        }
    }

    struct Err
    {
        public TextPosition errorPosition;
        public byte errorCode;

        public Err(TextPosition errorPosition, byte errorCode)
        {
            this.errorPosition = errorPosition;
            this.errorCode = errorCode;
        }
    }

    class InputOutput
    {
        const byte ERRMAX = 9;
        public static char Ch { get; set; }
        public static TextPosition positionNow;
        public static string? line;
        public static byte lastInLine = 0;
        public static List<Err> err = new List<Err>();
        public static StreamReader? File { get; set; }
        private static uint errCount = 0;
        public static uint ErrCount => errCount;

        static InputOutput()
        {
            positionNow = new TextPosition(1, 0);
            err = new List<Err>();
        }

        static public void NextCh()
        {
            if (line == null) return;

            // Переход на новую строку
            if (positionNow.charNumber >= lastInLine)
            {
                ListThisLine();
                if (err.Count > 0)
                    ListErrors();

                ReadNextLine();
                positionNow.lineNumber++;
                positionNow.charNumber = 0;

                if (line == null || line.Length == 0)
                {
                    Ch = '\0';
                    return;
                }
            }
            else
            {
                positionNow.charNumber++;
            }

            if (line != null && positionNow.charNumber < line.Length)
                Ch = line[positionNow.charNumber];
            else
                Ch = '\0';
        }

        private static void ListThisLine()
        {
            if (line != null)
                Console.WriteLine(line);
        }

        private static void ReadNextLine()
        {
            if (File != null && !File.EndOfStream)
            {
                line = File.ReadLine();
                if (line == null)
                {
                    line = "";
                    lastInLine = 0;
                }
                else
                {
                    lastInLine = (byte)(line.Length > 0 ? line.Length - 1 : 0);
                }
                err = new List<Err>();
            }
            else
            {
                End();
            }
        }

        static void End()
        {
            Console.WriteLine($"Компиляция завершена: ошибок — {errCount}!");
        }

        static void ListErrors()
        {
            if (line == null) return;

            foreach (Err item in err)
            {
                ++errCount;
                string s = new string(' ', (int)item.errorPosition.charNumber);
                s = s.Substring(0, Math.Min(s.Length, line.Length));
                Console.WriteLine($"{s}^ ошибка код {item.errorCode}");
            }
        }

        static public void Error(byte errorCode, TextPosition position)
        {
            if (err.Count <= ERRMAX)
            {
                err.Add(new Err(position, errorCode));
            }
        }

        public static char Peek() 
        {
            if (line == null) return '\0';
            if (positionNow.charNumber + 1 < line.Length)
                return line[positionNow.charNumber + 1];
            return '\0';
        }
    }
}