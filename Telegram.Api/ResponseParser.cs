namespace Telegram.Api
{
    public class ResponseParser
    {
        public bool HasErrors { get { return _error != null; } }
        public Error Error { get { return _error; } }

        private Error _error;
        private readonly JsonParser _parser;

        public ResponseParser()
        {
            _parser = new JsonParser();
        }
            
        public T Parse<T>(string inputString) where T : class
        {
            var token = _parser.GetToken(inputString, "ok");

            if (!bool.Parse(token))
            {
                _error = _parser.Parse<Error> (inputString);

                return null;
            }

            var result = _parser.GetToken(inputString, "result");
            return _parser.Parse<T>(result);
        }
    }

    public class Error
    {
        public int ErrorCode { get; set; }
        public string Description { get; set; }
    }
}
