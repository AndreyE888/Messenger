namespace Messenger.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
        public ICollection<Chat> Chats { get; set; } = new List<Chat>();
        public ICollection<ChatUser> ChatUsers { get; set; } = new List<ChatUser>(); 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
