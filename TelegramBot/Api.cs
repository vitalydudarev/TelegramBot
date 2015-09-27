using System.Collections.Generic;

namespace TelegramBot
{
    public class Api
    {
        public const string AccessToken = @"";
        public string Uri = string.Format(@"https://api.telegram.org/bot{0}/", AccessToken);

        public string GetUpdates()
        {
            var getRequest = new Request().SendGetRequest(Uri + "getUpdates");
            var response = new Response().GetResponse(getRequest);
            return response;
        }

        public void SendMessage(int userId, string message)
        {
            var dict = new Dictionary<string, string>
			{ 
				{"chat_id", userId.ToString()},
				{"text", message},
			};
            var request = new Request().SendPostRequest(Uri + "sendMessage", dict);
            var response = new Response().GetResponse(request);
        }
    }
}
