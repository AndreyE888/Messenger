namespace Messenger.Server.DTOs
{
    public class SendMessageRequest
    {
        public string Text { get; set; } = string.Empty;
        public int ChatId { get; set; }
        
    }
}
