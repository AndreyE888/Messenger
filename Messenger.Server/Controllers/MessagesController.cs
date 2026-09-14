using Messenger.Application.Interfaces;
using Messenger.Domain.Entities;
using Messenger.Server.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Messenger.Server.Controllers
{
    [ApiController]
    [Route("api/messages")]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        public MessagesController(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return Unauthorized("Пользователь не найден!");
            }

            var message = new Message
            {
                Text = request.Text,
                UserId = userId,
                ChatId = request.ChatId,
            };

            await _messageRepository.AddAsync(message);
            return Ok(new
            {
                Message = "Сообщение успешно отправлено!",
                MessageId = message.Id,
                SentAt = message.SentAt
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetMessages([FromQuery] int chatId)
        {
            var messages = await _messageRepository.GetMessagesByChatIdAsync(chatId);
            return Ok(messages);
        }
    }
}