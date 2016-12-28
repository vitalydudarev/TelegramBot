using System;
using System.IO;
using Newtonsoft.Json;

namespace Common
{
    public class Config
    {
        public string AccessToken { get; set; }
        public int? BotOwnerId { get; set; }
        public string LogsDirectory { get; set; }
        public string DownloadsDirectory { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(AccessToken))
                throw new Exception("Access token is not provided.");
        }

        public static Config Get(string fileName)
        {
            var text = File.ReadAllText(fileName);

            return JsonConvert.DeserializeObject<Config>(text);
        }
    }
}
