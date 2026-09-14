using System.Collections.Generic;

namespace Messenger.Domain.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
