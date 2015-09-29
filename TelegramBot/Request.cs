using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace TelegramBot
{
    public abstract class Request
    {
        protected string _uri;

        public Request(string uri)
        {
            _uri = uri;
        }
            
        public abstract string Send();

        protected string GetResponse(HttpWebRequest request)
        {
            Stream responseStream;

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                responseStream = response.GetResponseStream();
            }
            catch (WebException e)
            {
                responseStream = e.Response.GetResponseStream();
            }
                
            var responseString = new StreamReader(responseStream).ReadToEnd();
            return responseString;
        }
    }

    public class PostRequest : Request
    {
        private Dictionary<string, string> _parameters;

        public PostRequest(string uri, Dictionary<string, string> parameters) : base(uri)
        {
            _parameters = parameters;
        }
            
        public override string Send()
        {
            var request = (HttpWebRequest)WebRequest.Create(_uri);

            string post = "";

            foreach (var parameter in _parameters)
            {
                post += parameter.Key + "=" + parameter.Value + "&";
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
                
            return GetResponse(request);
        }
    }

    public class GetRequest : Request
    {
        public GetRequest(string uri) : base(uri)
        {
        }
            
        public override string Send()
        {
            var request = (HttpWebRequest)WebRequest.Create(_uri);
            return GetResponse(request);
        }
    }
}
