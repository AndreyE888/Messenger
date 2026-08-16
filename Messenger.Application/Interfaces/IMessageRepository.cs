using Messenger.Domain.Entities;


namespace Messenger.Application.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message?> GetMessageByIdAsync(int id);
        Task<IEnumerable<Message>> GetMessagesByUserIdAsync(int userid);
        Task AddAsync(Message message);


    }
}
