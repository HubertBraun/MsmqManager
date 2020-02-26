using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsmqManager
{
    public class Menu
    {
        private List<string> _actions;
        private List<string> _extraActions;
        public int CurrentAction { get; private set; }
        public int ActionCount { get { return _actions.Count + _extraActions.Count; } }
        public Menu(List<string> actions, List<string> extraActions)
        {
            _actions = actions;
            _extraActions = extraActions;
            CurrentAction = 0;
        }
        public void MoveUp()
        {
            CurrentAction--;
            if (CurrentAction < 0)
                CurrentAction = ActionCount - 1;
        }
        public void MoveDown()
        {
            CurrentAction++;
            if (CurrentAction >= ActionCount)
                CurrentAction = 0;
        }
        public void DisplayMenu(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            for (var i = 0; i < ActionCount; i++)
            {
                if (CurrentAction == i)
                    ConsoleHelper.SetInversedColors();
                if (i < _actions.Count)
                    Console.Write(_actions[i]);
                else
                    Console.Write(_extraActions[i - _actions.Count]);
                if (CurrentAction == i)
                    Console.ResetColor();
                Console.WriteLine("".PadRight(50, ' '));
            }
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
