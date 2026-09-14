using Messenger.Application.Interfaces;
using Messenger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Infrastructure.Repositories
{
    public class MessageRepository: IMessageRepository
    {
        private readonly AppDbContext _context;
        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Message?> GetMessageByIdAsync(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Message>> GetMessagesByUserIdAsync(int userId)
        {
            return await _context.Messages.Where(m => m.UserId == userId).Include(m => m.User).OrderByDescending(m => m.SentAt).ToListAsync();
        }

        public async Task AddAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetMessagesByChatIdAsync(int chatId)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatId)
                .Include(m => m.User)
                .OrderByDescending(m => m.SentAt)
                .ToListAsync();
        }

    }
}
