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
            var request = new GetRequest(_uri + "getUpdates");
            var responseString = request.Send();
            return responseString;
        }

        public void SendMessage(int userId, string message)
        {
            var parameters = new Dictionary<string, string>
            { 
                {"chat_id", userId.ToString()},
                {"text", message},
            };
            var request = new PostRequest(_uri + "sendMessage", parameters);
            var responseString = request.Send();
        }
    }
}
