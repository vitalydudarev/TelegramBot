using System;

namespace Telegram.Bot
{
    public class UpdateProcessor
    {
        public UpdateProcessor()
        {
        }

        public ResponseMessage ProcessIncoming(Api.Update update)
        {
            var message = update.Message;
            if (message != null) 
            {
                return new ResponseMessage(update.Message.Chat.Id, "You said: " + message.Text);
            }

            return null;
        }
    }

    public class ResponseMessage
    {
        public int ChatId { get; set; }
        public string Message { get; set; }

        public ResponseMessage(int chatId, string message)
        {
            ChatId = chatId;
            Message = message;
        }
    }
}

