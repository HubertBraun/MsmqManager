using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MsmqManager.ConsoleHelper;
namespace MsmqManager
{
    public class Menu
    {
        private List<string> _actions;
        private List<string> _extraActions;
        private int _maxOptionIndex;
        public int CurrentY { get; private set; }
        public int CurrentAction { get; private set; }
        public int ActionCount { get { return _actions.Count + _extraActions.Count; } }
        public Menu(List<string> actions, List<string> extraActions, int maxOptions)
        {
            _actions = actions;
            _extraActions = extraActions;
            CurrentAction = 0;
            _maxOptionIndex = maxOptions - 2;
            CurrentY = 0;
        }
        public void MoveUp()
        {
            CurrentAction--;
            if (CurrentAction < 0)
            {
                CurrentAction = ActionCount - 1;
                CurrentY = ActionCount - _maxOptionIndex - 2;
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
        public void DisplayMenu(int x, int y)
        {
            if (CurrentY > 0 && CurrentAction == CurrentY + 1)
                CurrentY--;

            if (CurrentY < ActionCount - _maxOptionIndex - 2 && CurrentAction == _maxOptionIndex + CurrentY)
                CurrentY++;
            var it = CurrentY;
            Console.SetCursorPosition(x, y);
            if (CurrentY > 0)
            {
                Console.WriteLine("...".PadRight(MaxQueueName + 10));
                it++;
            }
            for (var i = it; i < ActionCount; i++)
            {
                if (CurrentAction == i)
                    ConsoleHelper.SetInversedColors();
                if (i < _actions.Count)
                    Console.Write(_actions[i]);
                else
                    Console.Write(_extraActions[i - _actions.Count]);
                if (CurrentAction == i)
                    Console.ResetColor();
                Console.WriteLine("".PadRight(MaxQueueName + 10, ' '));
                if (i == _maxOptionIndex + CurrentY && CurrentY < ActionCount - _maxOptionIndex - 2)
                {
                    Console.WriteLine("...".PadRight(MaxQueueName + 10));
                    break;
                }
            }
        }

        public string ReadStringFromUser()
        {
            Console.ResetColor();
            Console.SetCursorPosition(0, CurrentAction - CurrentY + 1);
            Console.WriteLine("".PadLeft(MaxQueueName, '.'));
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
            _actions = actions;
            CurrentAction = it;
        }
        public void AddAction(string str)
        {
            _actions.Add(str);
        }
    }
}
