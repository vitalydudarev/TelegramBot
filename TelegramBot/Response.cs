using System.IO;
using System.Net;

namespace TelegramBot
{
    public class Response
    {
        public string GetResponse(HttpWebRequest request)
        {
            var response = (HttpWebResponse)request.GetResponse();
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}
