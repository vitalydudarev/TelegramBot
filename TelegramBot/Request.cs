using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TelegramBot
{
    public class Request
    {
        public HttpWebRequest SendGetRequest(string url)
        {
            return (HttpWebRequest)WebRequest.Create(url);
        }

        public HttpWebRequest SendPostRequest(string url, Dictionary<string, string> pairs)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            string post = "";

            foreach (var pair in pairs)
            {
                post += pair.Key + "=" + pair.Value + "&";
            }

            post = post.Remove(post.Length - 1);

            var data = Encoding.ASCII.GetBytes(post);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            return request;
        }
    }
}
