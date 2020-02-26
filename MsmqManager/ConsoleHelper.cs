using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsmqManager
{
    public static class ConsoleHelper
    {
        public static void SetInversedColors()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        public static string ReadStringFromUser(int x, int y)
        {
            Console.ResetColor();
            Console.SetCursorPosition(x, y);
            Console.WriteLine("".PadLeft(50, '.'));
            Console.SetCursorPosition(x, y);
            var keyInfo = new ConsoleKeyInfo();
            var builder = new StringBuilder();
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                keyInfo = Console.ReadKey();
                if(keyInfo.Key!= ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Backspace)
                {
                    builder.Append(keyInfo.KeyChar);
                }
                if(keyInfo.Key == ConsoleKey.Backspace)
                {
                    builder.Remove(builder.Length - 1, 1);
                    Console.Write(".");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                }
            }
            return builder.ToString();
        }

    }
}
