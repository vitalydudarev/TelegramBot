using System.IO;
using Telegram.Api;

namespace Telegram.Bot
{
    public class UpdateProcessor
    {
        private readonly Api.Api _api;
        private readonly string _downloadsDirectory;

        public UpdateProcessor(Api.Api api, string downloadsDirectory)
        {
            _api = api;
            _downloadsDirectory = downloadsDirectory;
        }

        public void Process(Update update)
        {
            var message = update.Message;
            if (message != null)
            {
                if (message.Photo != null)
                    DownloadFile(message.Photo[message.Photo.Length - 1].FileId, message.Chat.Id);

                if (message.Document != null)
                    DownloadFile(message.Document.FileId, message.Chat.Id, message.Document.FileName);
            }
        }

        private void DownloadFile(string fileId, int senderId, string fileName = null)
        {
            var file = _api.GetFile(fileId);
            var fileBytes = _api.DownloadFile(file.FilePath);
            
            var fileType = file.FilePath.Split('/')[0];
            if (string.IsNullOrEmpty(fileName))
                fileName = file.FilePath.Split('/')[1];

            var destinationFolder = Path.Combine(_downloadsDirectory, senderId.ToString(), fileType);
            
            if (!Directory.Exists(destinationFolder))
                Directory.CreateDirectory(destinationFolder);

            System.IO.File.WriteAllBytes(Path.Combine(destinationFolder, fileName), fileBytes);
        }
    }
}

