using System.IO;
using System.Net;

namespace TelegramBot
{
    public class Response<T> where T : class
    {
        public bool HasErrors { get { return _error != null; } }
        public Error Error { get { return _error; } }
        public T Result { get {return GetResult(); } }

        private string _responseString;
        private Error _error;
        private JsonParser _parser;

        public Response(string responseString)
        {
            _responseString = responseString;
            _parser = new JsonParser();
        }
            
        private T GetResult()
        {
            var token = _parser.GetToken(_responseString, "ok");

            if (!bool.Parse(token))
            {
                _error = _parser.Parse<Error> (_responseString);

                return (T)null;
            }
            
            return _parser.Parse<T>(_responseString);
        }
    }

    public class Error
    {
        public int ErrorCode { get; set; }
        public string Description { get; set; }
    }
}
