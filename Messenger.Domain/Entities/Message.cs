
namespace Messenger.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; } = string.Empty;
        public User? User { get; set; }
        public DateTime SentAt { get; set; }

        public Message() => SentAt = DateTime.UtcNow;

    }
}
