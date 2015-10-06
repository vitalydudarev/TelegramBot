namespace TelegramBot
{
    public class Update
    {
        public int UpdateId { get; set; }
        public Message Message { get; set; }
    }

    public class Message
    {
        public int MessageId { get; set; }
        public User From { get; set; }
        public int Date { get; set; }
        public string Text { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
    }

    public class Location
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}

