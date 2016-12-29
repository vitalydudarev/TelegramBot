using System;
using System.Threading;
using Common;

namespace Telegram.Bot
{
    class Program
    {
        private static BotService _botService;

        static void Main(string[] args)
        {
            var config = Config.Get("config.json");
            config.Validate();

            _botService = new BotService(config.AccessToken, config);

            var thread = new Thread(Run);
            thread.Start();

            var line = Console.ReadLine();
            if (line == "s")
                _botService.Stop();
        }

        private static void Run()
        {
            _botService.Start();
        }
    }
}
