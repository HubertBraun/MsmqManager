using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsmqManager
{
    public class Help
    {
        private List<Tuple<string, string>> _pairs;
        public Help()
        {
            _pairs = new List<Tuple<string, string>>();
            _pairs.Add(new Tuple<string, string>("Enter", "Select queue"));
            _pairs.Add(new Tuple<string, string>("A", "Add message"));
            _pairs.Add(new Tuple<string, string>("N", "Add N empty messagea"));
            _pairs.Add(new Tuple<string, string>("D", "Delete queue"));
            _pairs.Add(new Tuple<string, string>("P", "Delete messages"));
            _pairs.Add(new Tuple<string, string>("Q", "Exit"));
        }
        public void DisplayHelp(int y)
        {
            Console.SetCursorPosition(0, y);
            foreach (var p in _pairs)
            {
                ConsoleHelper.SetInversedColors();
                Console.Write(p.Item1);
                Console.ResetColor();
                Console.Write(" " + p.Item2 + " ");
            }
        }
    }
}
