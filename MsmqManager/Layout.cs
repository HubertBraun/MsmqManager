using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsmqManager
{
    public class Layout
    {
        private string _title;
        public Menu Menu { get; private set; }
        private Help _help;
        private int _height;
        private string _exception = "";
        public Layout(string title, Menu menu, Help help, int height)
        {
            Menu = menu;
            _title = title;
            _help = help;
            _height = height;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(_title);
            Menu.DisplayMenu(0, 1);
            _help.DisplayHelp(_height);
        }
        public void Refersh()
        {
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(_title);
            Menu.DisplayMenu(0, 1);
            Console.SetCursorPosition(0, Menu.ActionCount + 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(_exception);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Black;
        }
        public void CleanException()
        {
            _exception = "".PadLeft(100, ' ');
        }
        public void SetException(string msg)
        {
            _exception = msg;
            Console.SetCursorPosition(0, Menu.ActionCount + 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
