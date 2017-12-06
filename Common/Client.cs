using System.IO;
using System.Net;

namespace Common
{
    public interface IClient
    {
        string Send(Request request);
    }

    public class Client : IClient
    {
        private const int MaxTransferSize = 20 * 1024 * 1024;
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

        public byte[] SendSync(Request request)
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

            using (BinaryReader reader = new BinaryReader(responseStream))
            {
                var bytes = reader.ReadBytes(MaxTransferSize);
                responseStream.Close();

                return bytes;
            }
        }
    }
}