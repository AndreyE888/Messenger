using Messenger.Application.Interfaces;
using Messenger.Domain.Entities;
using Messenger.Server.DTOs;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;


namespace Messenger.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (registerRequest == null || string.IsNullOrWhiteSpace(registerRequest.Email) ||
                string.IsNullOrWhiteSpace(registerRequest.Password) ||
                string.IsNullOrWhiteSpace(registerRequest.UserName))
            {
                return BadRequest("Не все поля регистрации заполнены!");
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(registerRequest.Email);
            if (existingUser != null)
            {
                return Conflict("Пользователь с таким email уже существует!");
            }

            if (registerRequest.Password.Length < 6)
            {
                return BadRequest("Длина пароля не может быть меньше 6!");
            }

            if (registerRequest.UserName.Length < 2)
            {
                return BadRequest("Длина имени пользователи не может быть меньше 2 символов!");
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);
            var user = new User()
            {
                UserName = registerRequest.UserName,
                PasswordHash = passwordHash,
                Email = registerRequest.Email
            };

            await _userRepository.AddAsync(user);

            return Ok(new { Message = "Успешно", UserId = user.Id });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest ==null || loginRequest.Email  == null || loginRequest.Password == null)
            {
                return BadRequest("Ошибка! Не все поля заполнены!");
            }
            if (loginRequest.Password.Length < 6)
            {
                return BadRequest("Длина пароля не может быть меньше 6!");
            }

            if (loginRequest.Email.Length < 4)
            {
                return BadRequest("Некорректный Email!");
            }
            var existingUser = await _userRepository.GetUserByEmailAsync(loginRequest.Email);
            if (existingUser == null)
            {
                return Unauthorized("Неверный Email или пароль!");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, existingUser.PasswordHash);
            
            if (!isPasswordValid)
            {
                return Unauthorized("Неверный Email или пароль!");
            }
            var token = JwtProvider.
              
    }
}
    