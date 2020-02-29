using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsmqManager
{
    public static class ConsoleHelper
    {
        public static readonly int WindowWidth = int.Parse(ConfigurationManager.AppSettings["WindowWidth"]);
        public static readonly int MaxQueueName = int.Parse(ConfigurationManager.AppSettings["MaxQueueName"]);
        public static void SetInversedColors()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
