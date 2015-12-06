using System;
using System.IO;
using System.Net;

namespace Telegram.Api
{
    public interface IClient
    {
        string Send(Request request);
    }

    public class Client : IClient
    {
        private readonly string _uri;

        public Client(string uri)
        {
            _uri = uri;
        }

        public string Send(Request request)
        {
            Stream responseStream;

            try
            {
                var response = (HttpWebResponse)request.CreateRequest(_uri).GetResponse();
                responseStream = response.GetResponseStream();
            }
            catch (WebException e)
            {
                responseStream = e.Response.GetResponseStream();
            }

            return responseStream == null ? null : new StreamReader(responseStream).ReadToEnd();
        }
    }
}

