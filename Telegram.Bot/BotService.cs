using System.Threading;
using Common;

namespace Telegram.Bot
{
    public class BotService
    {
        private bool _started;
        private readonly Api.Api _api;
        private readonly Bot _bot;
        private readonly UpdateLogger _updateLogger;

        public BotService(string accessToken, Config config)
        {
            _api = new Api.Api(accessToken);
            _bot = new Bot(_api, config.DownloadsDirectory);
            _updateLogger = new UpdateLogger(config.LogsDirectory);
        }

        public void Start()
        {
            _started = true;

            int offset = 0;
            
            while (_started)
            {
                var updates = _api.GetUpdates(offset);

                foreach (var update in updates)
                {
                    _bot.Process(update);
                    _updateLogger.Write(update);

                    offset = update.UpdateId + 1;
                }

                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            _started = false;
            _updateLogger.Dispose();
        }
    }
}
