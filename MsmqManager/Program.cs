using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using MsmqManager.Helper;
using MsmqManager.TUI;
using static MsmqManager.Helper.ConsoleHelper;
namespace MsmqManager
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            var mng = new QueueManager();
            var help = new Help(new Coords(WindowWidth, 2, 0, WindowHeight - 2));
            var menu = new Menu(new Coords(MaxQueueName + 8, WindowHeight - 3, 0, 1), mng.GetQueueNamesWithCount(), new List<string> { "Add Queue" });
            var msgBox = new TextBox(new Coords(WindowWidth - MaxQueueName - 10, WindowHeight - 3, MaxQueueName + 10, 1));
            var layout = new Layout("QUEUE MANAGER v1.0.0.0");
            layout.AddElement("menu", menu);
            layout.AddElement("help", help);
            layout.AddElement("msgBox", msgBox);
            bool waitForChooseSecondQueue = false;
            bool isCopy = false;
            var from = 0;
            bool run = true;
            while (run)
            {
                try
                {
                    //menu.UpdateMenu(mng.GetQueueNamesWithCount(), menu.CurrentAction);
                    layout.Display();
                    ConsoleKey key = Console.ReadKey().Key;
                    layout.CleanException();
                    switch (key)
                    {
                        case ConsoleKey.Q:
                            if (waitForChooseSecondQueue)
                            {
                                waitForChooseSecondQueue = false;
                                help.SetStandardPairs();
                            }
                            else
                                run = false;
                            break;
                        case ConsoleKey.Enter:
                            if (menu.ActionCount - 1 == menu.CurrentAction)
                            {
                                var name = menu.ReadStringFromUser();
                                mng.AddQueue(name);
                                menu.UpdateMenu(mng.GetQueueNamesWithCount(), menu.CurrentAction);
                                menu.MoveDown();
                                layout.Display();
                            }
                            break;
                        case ConsoleKey.D:
                            if (menu.CurrentAction < menu.ActionCount - 1)
                            {
                                mng.DeleteQueue(menu.CurrentAction);
                                menu.UpdateMenu(mng.GetQueueNamesWithCount(), menu.CurrentAction);
                                if (menu.CurrentY > 0)
                                    menu.MoveUp();
                                layout.Display();
                            }
                            break;
                        case ConsoleKey.A:
                            if (menu.CurrentAction < menu.ActionCount - 1)
                            {
                                var msg = menu.ReadStringFromUser();
                                mng.AddMessage(menu.CurrentAction, msg);
                            }
                            break;
                        case ConsoleKey.N:
                            if (menu.CurrentAction < menu.ActionCount - 1)
                            {
                                var count = int.Parse(menu.ReadStringFromUser());
                                for (int i = 0; i < count; i++)
                                    mng.AddMessage(menu.CurrentAction, " ");
                            }
                            break;
                        case ConsoleKey.P:
                            if (menu.CurrentAction < menu.ActionCount - 1)
                            {
                                mng.DeleteMessages(menu.CurrentAction);
                            }
                            break;
                        case ConsoleKey.C:
                            if (menu.CurrentAction < menu.ActionCount - 1)
                            {
                                if (!waitForChooseSecondQueue)
                                {
                                    from = menu.CurrentAction;
                                    isCopy = true;
                                    waitForChooseSecondQueue = !waitForChooseSecondQueue;
                                    help.SetCopyPairs();
                                }
                                else if (isCopy)
                                {
                                    var to = menu.CurrentAction;
                                    mng.CopyMessages(from, to);
                                    waitForChooseSecondQueue = !waitForChooseSecondQueue;
                                    help.SetStandardPairs();
                                }

                            }
                            break;
                        case ConsoleKey.M:
                            if (menu.CurrentAction < menu.ActionCount - 1)
                            {
                                if (!waitForChooseSecondQueue)
                                {
                                    from = menu.CurrentAction;
                                    isCopy = false;
                                    waitForChooseSecondQueue = !waitForChooseSecondQueue;
                                    help.SetMovePairs();
                                }
                                else if (!isCopy)
                                {
                                    var to = menu.CurrentAction;
                                    mng.MoveMessages(from, to);
                                    waitForChooseSecondQueue = !waitForChooseSecondQueue;
                                    help.SetStandardPairs();
                                }
                            }
                            break;
                        case ConsoleKey.T:
                            if (menu.CurrentAction < menu.ActionCount - 1)
                            {
                                var msg = mng.ReadTopMessage(menu.CurrentAction);
                                msgBox.SetText(msg);
                            }
                            break;
                        case ConsoleKey.R:
                            menu.UpdateMenu(mng.GetQueueNamesWithCount(), menu.CurrentAction);
                            break;
                        case ConsoleKey.DownArrow:
                            menu.MoveDown();
                            break;
                        case ConsoleKey.UpArrow:
                            menu.MoveUp();
                            break;
                    }
                }
                catch (Exception e)
                {
                    var coords = new Coords(menu.Coords.Size.X, 0, 0, menu.CurrentAction + menu.Coords.Position.Y - menu.CurrentY);
                    layout.SetException(e.Message, coords);
                }
            }
        }
    }
}
