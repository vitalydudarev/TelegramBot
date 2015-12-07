using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Telegram.Bot
{
    class Program
    {
        private static Bot _bot;

        static void Main(string[] args)
        {
            _bot = new Bot();

            var thread = new Thread(Run);
            thread.Start();

            var line = Console.ReadLine();
            if (line == "s")
                _bot.Stop();
        }

        private static void Run()
        {
            _bot.Start();
        }
    }
}
