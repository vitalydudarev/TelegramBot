using System.Threading;
using Common;

namespace Telegram.Bot
{
    public class Bot
    {
        private bool _started;
        private readonly Api.Api _api;
        private readonly UpdateProcessor _updateProcessor;
        private readonly UpdateLogger _updateLogger;

        public Bot(string accessToken, Config config)
        {
            _api = new Api.Api(accessToken);
            _updateProcessor = new UpdateProcessor(_api, config.DownloadsDirectory);
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
                    _updateProcessor.Process(update);
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
