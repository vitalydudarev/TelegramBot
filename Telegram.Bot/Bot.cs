﻿using System.Threading;

namespace Telegram.Bot
{
    public class Bot
    {
        private bool _started;
        private readonly Api.Api _api;

        public Bot()
        {
            _api = new Api.Api("");
        }

        public void Start()
        {
            _started = true;

            int offset = 0;
            var updateProcessor = new UpdateProcessor();

            while (_started)
            {
                var updates = _api.GetUpdates(offset);

                foreach (var update in updates)
                {
                    var response = updateProcessor.ProcessIncoming(update);
                    if (response != null)
                        _api.SendMessage(response.ChatId, response.Message);

                    offset = update.UpdateId + 1;
                }

                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            _started = false;
        }
    }
}
