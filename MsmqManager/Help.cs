using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MsmqManager.ConsoleHelper;

namespace MsmqManager
{
    public class Help
    {
        private List<Tuple<string, string>> _pairs;
        private int _height;
        public Help(int height)
        {
            _height = height;
            SetStandardPairs();
        }
        public void SetStandardPairs()
        {
            _pairs = new List<Tuple<string, string>>();
            _pairs.Add(new Tuple<string, string>("Enter", "Select queue"));
            _pairs.Add(new Tuple<string, string>("A", "Add message"));
            _pairs.Add(new Tuple<string, string>("N", "Add N empty messages"));
            _pairs.Add(new Tuple<string, string>("D", "Delete queue"));
            _pairs.Add(new Tuple<string, string>("P", "Delete messages"));
            _pairs.Add(new Tuple<string, string>("C", "Copy messages"));
            _pairs.Add(new Tuple<string, string>("M", "Move messages"));
            _pairs.Add(new Tuple<string, string>("Q", "Exit"));
        }
        public void DisplayHelp()
        {
            Console.SetCursorPosition(0, _height);
            foreach (var p in _pairs)
            {
                ConsoleHelper.SetInversedColors();
                Console.Write(p.Item1);
                Console.ResetColor();
                Console.Write(" " + p.Item2 + " ");
                if (Console.CursorLeft > WindowWidth - 10)
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
            }
        }
        public void SetMovePairs()
        {
            CleanHelp();
            _pairs = new List<Tuple<string, string>>();
            _pairs.Add(new Tuple<string, string>("M", "Move Messages"));
            _pairs.Add(new Tuple<string, string>("Q", "Break"));
        }

        public void SetCopyPairs()
        {
            CleanHelp();
            _pairs = new List<Tuple<string, string>>();
            _pairs.Add(new Tuple<string, string>("C", "Copy Messages"));
            _pairs.Add(new Tuple<string, string>("Q", "Break"));
        }
        private void CleanHelp()
        {
            Console.SetCursorPosition(0, _height);
            Console.WriteLine("".PadLeft(WindowWidth, ' '));
            Console.SetCursorPosition(0, _height + 1);
            Console.WriteLine("".PadLeft(WindowWidth, ' '));
        }

    }
}
