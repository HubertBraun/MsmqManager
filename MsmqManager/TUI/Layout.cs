using MsmqManager.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MsmqManager.Helper.ConsoleHelper;
namespace MsmqManager.TUI
{
    public class Layout : Drawable
    {
        private string _title;
        public Dictionary<string, Drawable> Elements { get; private set; }
        public Coords Coords { get; set; }

        private string _exception = "";

        public Layout(string title)
        {
            Elements = new Dictionary<string, Drawable>();
            _title = title.PadRight(WindowWidth);
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(_title);
            Console.ResetColor();

        }
        public void AddElement(string name, Drawable element)
        {
            Elements.Add(name, element);
        }
        public void Display()
        {
            foreach (var e in Elements)
            {
                e.Value.Display();
            }
            if (_exception.Any(x => x != ' '))
            {
                Console.SetCursorPosition(Coords.Position.X, Coords.Position.Y);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(_exception);
                Console.ResetColor();
            }
        }
        public void CleanException()
        {
            _exception = "".PadLeft(WindowWidth, ' ');
        }
        public void SetException(string msg, Coords excCoords)
        {
            Coords = excCoords;
            _exception = msg.PadRight(Coords.Size.X, ' ').Substring(0, Coords.Size.X);
        }

        public void Clean()
        {
            foreach(var e in Elements)
            {
                e.Value.Clean();
            }
        }
    }
}
