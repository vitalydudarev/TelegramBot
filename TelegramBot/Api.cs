using System.Collections.Generic;

namespace TelegramBot
{
    public class Api
    {
        private string _accessToken;
        private string _uri;

        public Api(string accessToken)
        {
            _accessToken = accessToken;
            _uri = @"https://api.telegram.org/bot" + _accessToken + "/";
        }

        public string GetUpdates()
        {
            var client = new Client(_uri + "getUpdates");
            return client.Send(new GetRequest());
        }

        public string SendMessage(int userId, string message)
        {
            var parameters = new Dictionary<string, string>
            { 
                { "chat_id", userId.ToString() },
                { "text", message },
            };

            var client = new Client(_uri + "sendMessage");
            return client.Send(new PostRequest(parameters));
        }
    }
}
