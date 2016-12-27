using System;
using System.Threading;
using Common;

namespace Telegram.Bot
{
    class Program
    {
        private static Bot _bot;

        static void Main(string[] args)
        {
            var config = Config.Get("config.json");
            config.Validate();

            _bot = new Bot(config.AccessToken, config);

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
