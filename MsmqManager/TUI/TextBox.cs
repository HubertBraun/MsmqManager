using MsmqManager.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsmqManager.TUI
{
    public class TextBox : Drawable
    {
        private string _text;
        public Coords Coords { get; set; }

        public TextBox(Coords coords)
        {
            Coords = coords;
            _text = "";
        }
        public void SetText(string text)
        {
            Clean();
            if (string.IsNullOrEmpty(text))
                _text = "<<Data is empty or it can't be readed>>";
            else
                _text = text;

            if (!string.IsNullOrEmpty(_text))
            {
                Console.SetCursorPosition(Coords.Position.X, Coords.Position.Y);
                for (int i = 0; i * Coords.Size.X <= _text.Length; i++)
                {
                    Console.Write(_text.Substring(i * Coords.Size.X, Math.Min(Coords.Size.X, _text.Length - i * Coords.Size.X)).PadRight(Coords.Size.X));
                    Console.SetCursorPosition(Coords.Position.X, Coords.Position.Y + i + 1);
                    if (Coords.Position.Y + i + 1 > Coords.Position.Y + Coords.Size.Y)
                        break;
                }
            }
        }

        public void Display()
        {
           // Display only on SetText event
        }
        public void Clean()
        {
            Console.SetCursorPosition(Coords.Position.X, Coords.Position.Y);
            var str = "".PadRight(Coords.Size.X);
            for(int i=0; i< Coords.Size.Y; i++)
            {
                Console.Write(str);
                Console.SetCursorPosition(Coords.Position.X, Coords.Position.Y + i + 1);
            }
        }

    }
}
