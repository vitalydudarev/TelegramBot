using System.Collections.Generic;

namespace Telegram.Api
{
    public class Api
    {
        private string _accessToken;
        private readonly string _uri;
        private readonly ResponseParser _responseParser;

        public Api(string accessToken)
        {
            _accessToken = accessToken;
            _uri = @"https://api.telegram.org/bot" + _accessToken + "/";
            _responseParser = new ResponseParser();
        }

        public Update[] GetUpdates(int offset = 0, int limit = 100, int timeout = 0)
        {
            var parameters = new Dictionary<string, string>
            {
                { "offset", offset.ToString() },
                { "limit", limit.ToString() },
                { "timeout", timeout.ToString() }
            };

            var client = new Client(_uri + "getUpdates");
            var request = new GetRequest(parameters);
            var response = client.Send(request);

            return _responseParser.Parse<Update[]>(response);
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
