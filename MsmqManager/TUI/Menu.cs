using MsmqManager.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MsmqManager.Helper.ConsoleHelper;
namespace MsmqManager.TUI
{
    public class Menu : Drawable
    {
        private List<string> _actions;
        private List<string> _extraActions;
        private int _maxOptionIndex;
        public int CurrentY { get; private set; }
        public int CurrentAction { get; private set; }
        public int ActionCount { get { return _actions.Count + _extraActions.Count; } }

        public Coords Coords { get; set; }

        public Menu(Coords coords, List<string> actions, List<string> extraActions)
        {
            _actions = actions;
            _extraActions = extraActions;
            CurrentAction = 0;
            CurrentY = 0;
            Coords = coords;
            _maxOptionIndex = coords.Size.Y - 2;
        }
        public void MoveUp()
        {
            CurrentAction--;
            if (CurrentAction < 0)
            {
                CurrentAction = ActionCount - 1;
                CurrentY = ActionCount - _maxOptionIndex - 2;
                if (CurrentY < 0)
                    CurrentY = 0;
            }
        }
        public void MoveToTheTop()
        {
            CurrentAction--;
            if (CurrentAction < 0)
            {
                CurrentAction = 0;
                CurrentY = 0;
            }
        }
        public void MoveDown()
        {
            CurrentAction++;
            if (CurrentAction >= ActionCount)
            {
                CurrentAction = 0;
                CurrentY = 0;
            }
        }
        public void Display()
        {
            if (CurrentY < 0)
                CurrentY = 0;
            if (CurrentY > 0 && CurrentAction == CurrentY + 1)
                CurrentY--;

            if (CurrentY < ActionCount - _maxOptionIndex - 2 && CurrentAction >= _maxOptionIndex + CurrentY)
                CurrentY++;
            var it = CurrentY;
            Console.SetCursorPosition(Coords.Position.X, Coords.Position.Y);
            if (CurrentY > 0)
            {
                Console.WriteLine("...".PadRight(Coords.Size.X));
                it++;
            }
            for (var i = it; i < ActionCount; i++)
            {
                if (CurrentAction == i)
                    SetInversedColors();
                if (i < _actions.Count)
                    Console.Write(_actions[i]);
                else
                    Console.Write(_extraActions[i - _actions.Count]);
                if (CurrentAction == i)
                    Console.ResetColor();
                if(i != ActionCount - 1)
                    Console.SetCursorPosition(Coords.Position.X, ++Console.CursorTop);
                if (i == _maxOptionIndex + CurrentY && CurrentY < ActionCount - _maxOptionIndex - 2)
                {
                    Console.Write("...".PadRight(Coords.Size.X));
                    break;
                }
            }
        }

        public string ReadStringFromUser()
        {
            Console.ResetColor();
            Console.SetCursorPosition(0, CurrentAction - CurrentY + 1);
            Console.WriteLine("".PadLeft(MaxQueueName + 1, '.'));
            Console.SetCursorPosition(0, CurrentAction - CurrentY + 1);
            var keyInfo = new ConsoleKeyInfo();
            var builder = new StringBuilder();
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Backspace)
                {
                    builder.Append(keyInfo.KeyChar);
                }
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    builder.Remove(builder.Length - 1, 1);
                    Console.Write(".");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                }
            }
            return builder.ToString();
        }

        public void UpdateMenu(List<string> actions, int it =0)
        {
            if (actions.Count < _actions.Count)
            {
                if (actions.Count > _maxOptionIndex)
                {
                    Console.SetCursorPosition(0, _maxOptionIndex + 2);
                    CurrentY--;
                }
                else
                    Console.SetCursorPosition(0, actions.Count + 2);
                Console.WriteLine("".PadLeft(Coords.Size.X));
            }
            _actions = actions;
            CurrentAction = it;
        }
        public void AddAction(string str)
        {
            _actions.Add(str);
        }


        public void Clean()
        {
            throw new NotImplementedException();
        }
    }
}
