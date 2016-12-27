using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Telegram.Api;
using File = System.IO.File;

namespace Telegram.Bot
{
    public class UpdateLogger : IDisposable
    {
        private readonly List<Update> _updateList = new List<Update>();
        private readonly string _logFileName;
        private int _queueLength;
        
        private const string FileName = "queue.json";
        private const int Length = 10;

        public UpdateLogger(string logsDirectory)
        {
            _logFileName = Path.Combine(logsDirectory, FileName);

            if (File.Exists(_logFileName))
            {
                var serialized = File.ReadAllText(_logFileName);
                _updateList = JsonConvert.DeserializeObject<List<Update>>(serialized);
            }
        }

        public void Write(Update update)
        {
            if (_queueLength == Length)
            {
                Save();
                _queueLength = 0;
            }

            _updateList.Add(update);
            _queueLength++;
        }

        public void Dispose()
        {
            if (_queueLength != 0)
                Save();
        }

        private void Save()
        {
            var serialized = JsonConvert.SerializeObject(_updateList, Formatting.Indented);
            File.WriteAllText(_logFileName, serialized);
        }
    }
}
