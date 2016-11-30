using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Telegram.Api
{
    public abstract class Request
    {
        protected Dictionary<string, string> _parameters;

        protected Request(Dictionary<string, string> parameters)
        {
            _parameters = parameters;
        }

        protected Request()
        {
        }

        public abstract WebRequest CreateRequest(string uri);
    }

    public class GetRequest : Request
    {
        public GetRequest()
        {
        }

        public GetRequest(Dictionary<string, string> parameters) : base(parameters)
        {
        }

        public override WebRequest CreateRequest(string uri)
        {
            string url = uri;

            if (_parameters != null && _parameters.Count > 0)
            {
                string post = "";

                foreach (var parameter in _parameters)
                {
                    post += parameter.Key + "=" + parameter.Value + "&";
                }

                post = post.Remove(post.Length - 1);

                url += "?" + post;
            }

            return (HttpWebRequest)WebRequest.Create(url);
        }
    }

    public class PostRequest : Request
    {
        public PostRequest(Dictionary<string, string> parameters) : base(parameters)
        {
        }

        public override WebRequest CreateRequest(string uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);

            string post = "";

            foreach (var parameter in _parameters)
            {
                post += parameter.Key + "=" + parameter.Value + "&";
            }

            post = post.Remove(post.Length - 1);

            var data = Encoding.UTF8.GetBytes(post);

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
