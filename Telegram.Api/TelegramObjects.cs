namespace Telegram.Api
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
        public Chat Chat { get; set; }
        public PhotoSize[] Photo { get; set; }
        public Document Document { get; set; }
    }

    public class Chat
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
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

    public class PhotoSize
    {
        public string FileId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FileSize { get; set; }
    }

    public class File
    {
        public string FileId { get; set; }
        public int FileSize { get; set; }
        public string FilePath { get; set; }
    }

    public class Document
    {
        public string FileId { get; set; }
        public PhotoSize Thumb { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public int FileSize { get; set; }
    }
}

