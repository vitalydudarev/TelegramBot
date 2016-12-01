using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Telegram.Api;
using File = System.IO.File;

namespace Telegram.Bot
{
    public class UpdateQueueTool : IDisposable
    {
        private readonly List<Update> _queue = new List<Update>();
        private int _queueLength;

        private const string FileName = "output/queue.json";
        private const int Length = 10;

        public UpdateQueueTool()
        {
            if (File.Exists(FileName))
            {
                var serialized = File.ReadAllText(FileName);
                _queue = JsonConvert.DeserializeObject<List<Update>>(serialized);
            }
        }

        public void Write(Update update)
        {
            if (_queueLength == Length)
            {
                var serialized = JsonConvert.SerializeObject(_queue, Formatting.Indented);
                File.WriteAllText(FileName, serialized);
                _queueLength = 0;
            }

            _queue.Add(update);
            _queueLength++;
        }

        public void Dispose()
        {
            if (_queueLength != 0)
            {
                var serialized = JsonConvert.SerializeObject(_queue, Formatting.Indented);
                File.WriteAllText(FileName, serialized);
            }
        }
    }
}
