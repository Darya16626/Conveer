using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Conveer
{
    internal class Redact
    {
        private int minStroka;
        private int maxStroka;

        public Redact(int min, int max)
        {
            minStroka = min;
            maxStroka = max + 1;
        }

        public string[] Show(string[] Text3)
        {
            int posTop = Console.CursorTop;
            int posLeft = Console.CursorLeft;
            ConsoleKeyInfo click2;
            char c;
            string cl;
            do
            {
                click2 = Console.ReadKey();
                if (click2.Key == ConsoleKey.UpArrow && posTop != minStroka)
                {
                    posTop--;
                    posLeft = 0;
                    Console.SetCursorPosition(posLeft, posTop);
                }
                if (click2.Key == ConsoleKey.DownArrow && posTop != maxStroka)
                {
                    posTop++;
                    posLeft = 0;
                    Console.SetCursorPosition(posLeft, posTop);
                }
                if (click2.Key == ConsoleKey.LeftArrow && posLeft > 0)
                {
                    posLeft--;
                    Console.SetCursorPosition(posLeft, posTop);
                }
                if (click2.Key == ConsoleKey.RightArrow && posLeft != Text3[posTop - 2].Length)
                {
                    posLeft++;
                    Console.SetCursorPosition(posLeft, posTop);
                }
                if (click2.Key == ConsoleKey.Backspace && posLeft > 0)
                {
                    //string t = Text3[posTop - 2].Remove(posLeft, 1);
                    posLeft--;
                    Text3[posTop - 2] = Text3[posTop - 2].Remove(posLeft, 1);
                    if (posLeft <= Text3[posTop - 2].Length - 1)
                    {
                        Text3[posTop - 2] = Text3[posTop - 2].Insert(posLeft, " ");
                    }
                    Console.SetCursorPosition(posLeft, posTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(posLeft, posTop);
                }
                if (Convert.ToInt32(click2.Key) >= 48 && Convert.ToInt32(click2.Key) <= 241)
                {
                    do
                    {
                        if (posLeft < Text3[posTop - 2].Length)
                        {
                            Text3[posTop - 2] = Text3[posTop - 2].Remove(posLeft, 1);
                        }
                        Text3[posTop - 2] = Text3[posTop - 2].Insert(posLeft, click2.KeyChar.ToString());
                        posLeft++;
                        Console.SetCursorPosition(0, posTop);
                        Console.Write(Text3[posTop - 2]);
                        Console.SetCursorPosition(posLeft, posTop);
                    }
                    while (Convert.ToInt32(click2.Key) >= 48 && Convert.ToInt32(click2.Key) >= 241);
                }
                Console.SetCursorPosition(posLeft, posTop);
            }
            while (click2.Key != ConsoleKey.Enter);
            return Text3;
        }
    }
}
