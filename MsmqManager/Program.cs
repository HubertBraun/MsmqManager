using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MsmqManager
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            var mng = new QueueManager();
            var layout = new Layout("QUEUE MANAGER", new Menu(mng.GetQueueNamesWithCount(), new List<string> { "Add Queue" }),
                new Help(), 28);

            var key = ConsoleKey.Enter;
            while (key != ConsoleKey.Q)
            {
                try
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("QUEUE MANAGER");
                    layout.Menu.UpdateMenu(mng.GetQueueNamesWithCount(), layout.Menu.CurrentAction);
                    layout.Refersh();
                    key = Console.ReadKey().Key;
                    layout.CleanException();
                    switch (key)
                    {
                        case ConsoleKey.Enter:
                            if (layout.Menu.ActionCount - 1 == layout.Menu.CurrentAction)
                            {
                                var name = ConsoleHelper.ReadStringFromUser(0, layout.Menu.CurrentAction + 1);
                                mng.AddQueue(name);
                            }
                            break;
                        case ConsoleKey.D:
                            if (layout.Menu.CurrentAction < layout.Menu.ActionCount - 1)
                            {
                                mng.DeleteQueue(layout.Menu.CurrentAction);
                            }
                            break;
                        case ConsoleKey.A:
                            if (layout.Menu.CurrentAction < layout.Menu.ActionCount - 1)
                            {
                                var msg = ConsoleHelper.ReadStringFromUser(0, layout.Menu.CurrentAction + 1);
                                mng.AddMessage(layout.Menu.CurrentAction, msg);
                            }
                            break;
                        case ConsoleKey.N:
                            if (layout.Menu.CurrentAction < layout.Menu.ActionCount - 1)
                            {
                                var count = int.Parse(ConsoleHelper.ReadStringFromUser(0, layout.Menu.CurrentAction + 1));
                                for (int i = 0; i < count; i++)
                                    mng.AddMessage(layout.Menu.CurrentAction, " ");
                            }
                            break;
                        case ConsoleKey.P:
                            if (layout.Menu.CurrentAction < layout.Menu.ActionCount - 1)
                            {
                                mng.DeleteMessages(layout.Menu.CurrentAction);
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            layout.Menu.MoveDown();
                            break;
                        case ConsoleKey.UpArrow:
                            layout.Menu.MoveUp();
                            break;
                    }
                }
                catch(Exception e)
                {
                    layout.SetException(e.Message);
                }
            }
        }
    }
}
