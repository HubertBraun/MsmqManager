using MsmqManager.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MsmqManager.Helper.ConsoleHelper;

namespace MsmqManager.TUI
{
    public class Help : Drawable
    {
        private List<Tuple<string, string>> _pairs;

        public Coords Coords { get; set; }

        public Help(Coords coords)
        {
            Coords = coords;
            SetStandardPairs();
            UpdatePairsOnScreen();
        }
        public void SetStandardPairs()
        {
            _pairs = new List<Tuple<string, string>>();
            _pairs.Add(new Tuple<string, string>("Enter", "Select queue"));
            _pairs.Add(new Tuple<string, string>("A", "Add message"));
            _pairs.Add(new Tuple<string, string>("R", "Refresh"));
            _pairs.Add(new Tuple<string, string>("N", "Add N empty messages"));
            _pairs.Add(new Tuple<string, string>("T", "Read top message"));
            _pairs.Add(new Tuple<string, string>("H", "Delete top message"));
            _pairs.Add(new Tuple<string, string>("D", "Delete queue"));
            _pairs.Add(new Tuple<string, string>("P", "Delete messages"));
            _pairs.Add(new Tuple<string, string>("C", "Copy messages"));
            _pairs.Add(new Tuple<string, string>("M", "Move messages"));
            _pairs.Add(new Tuple<string, string>("Q", "Exit"));
            UpdatePairsOnScreen();
        }
        private void UpdatePairsOnScreen()
        {
            var nextRow = 0;
            Console.SetCursorPosition(Coords.Position.X, Coords.Position.Y);
            for (int i = 0; i < _pairs.Count; i++)
            {
                SetInversedColors();
                Console.Write(_pairs[i].Item1);
                Console.ResetColor();
                Console.Write(" " + _pairs[i].Item2 + " ");
                if (i < _pairs.Count - 1 && _pairs[i + 1].Item2.Length + Console.CursorLeft > Coords.Position.X + Coords.Size.X)
                    Console.SetCursorPosition(Coords.Position.X, Coords.Position.Y + ++nextRow);
            }
        }
        
        public void Display()
        {
            // Display only on set pairs event
        }
        public void SetMovePairs()
        {
            Clean();
            _pairs = new List<Tuple<string, string>>();
            _pairs.Add(new Tuple<string, string>("M", "Move Messages"));
            _pairs.Add(new Tuple<string, string>("Q", "Break"));
            UpdatePairsOnScreen();
        }

        public void SetCopyPairs()
        {
            Clean();
            _pairs = new List<Tuple<string, string>>();
            _pairs.Add(new Tuple<string, string>("C", "Copy Messages"));
            _pairs.Add(new Tuple<string, string>("Q", "Break"));
            UpdatePairsOnScreen();
        }
        public void Clean()
        {
            for (int i = 0; i < Coords.Size.Y; i++)
            {
                Console.SetCursorPosition(Coords.Position.X, Coords.Position.Y + i);
                Console.Write("".PadLeft(Coords.Size.X - 1, ' '));
            }
        }
    }
}
