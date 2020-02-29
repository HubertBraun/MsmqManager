using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MsmqManager.ConsoleHelper;
namespace MsmqManager
{
    public class Layout
    {
        private string _title;
        public Menu Menu { get; private set; }
        public Help Help { get; private set; }
        private string _exception = "";
        public Layout(string title, Menu menu, Help help)
        {
            Menu = menu;
            _title = title;
            Help = help;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(_title);
            Menu.DisplayMenu(0, 1);
            Help.DisplayHelp();
        }
        public void Refersh()
        {
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(_title.PadRight(WindowWidth, ' '));
            Console.ResetColor();
            Menu.DisplayMenu(0, 1);
            Console.SetCursorPosition(0, Menu.ActionCount - Menu.CurrentY - 2);
            Console.ForegroundColor = ConsoleColor.Red;
            if (_exception.Any(x => x != ' '))
            {
                Console.SetCursorPosition(0, Menu.CurrentAction - Menu.CurrentY + 1);
                Console.WriteLine(_exception);
            }
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Black;
            Help.DisplayHelp();
        }
        public void CleanException()
        {
            _exception = "".PadLeft(WindowWidth, ' ');
        }
        public void SetException(string msg)
        {
            _exception = msg.PadRight(WindowWidth, ' ');
            Console.SetCursorPosition(0, Menu.CurrentAction - Menu.CurrentY + 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
