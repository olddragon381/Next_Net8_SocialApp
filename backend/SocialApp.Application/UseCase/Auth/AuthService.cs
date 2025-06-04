using SocialApp.Application.DTOs;
using SocialApp.Application.Interfaces;
using SocialApp.Domain.Entities;
using SocialApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.UseCase.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<AuthResponeDTO> LoginAsync(LoginDTO loginDTO)
        {
            var checkUser = await _userRepository.GetByEmailAsync(loginDTO.Email);

            if (checkUser == null)
                throw new Exception("Tài khoản hoặc mật khẩu không đúng");

            Console.WriteLine("User nhập: " + loginDTO.PasswordHash);
            Console.WriteLine("Salt trong DB: " + checkUser.Salt);

            var hashedInput = new ProtectAuthService().hashPassword(loginDTO.PasswordHash, checkUser.Salt);
            Console.WriteLine("Hash tạo lại: " + hashedInput);
            Console.WriteLine("Hash trong DB: " + checkUser.PasswordHash);
            if (hashedInput != checkUser.PasswordHash)
                throw new Exception("Mật khẩu không đúng");

            var token = _jwtProvider.GenerateToken(checkUser);

            return new AuthResponeDTO
            {
                UserId = checkUser.Id,
                UserName = checkUser.Username,
                Email = checkUser.Email,
                Expiration = DateTime.UtcNow.AddHours(1),
                Token = token
            };
        }

        public async Task<AuthResponeDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            var checkUser = await _userRepository.GetByUsernameAsync(registerDTO.UserName);
            if (checkUser != null)
            {
                throw new Exception("User đã tồn tại");
            }
            var checkEmail = await _userRepository.GetByEmailAsync(registerDTO.Email);
            if (checkEmail != null)
            {
                throw new Exception("Email đã tồn tại");
            }
            var protectAuthService = new ProtectAuthService();
            var salt = protectAuthService.generateSalth();
            var hashPassword = protectAuthService.hashPassword(registerDTO.PasswordHash, salt);

            var user = new SocialApp.Domain.Entities.User // Fully qualify the User class to avoid namespace conflict
            {
                Username = registerDTO.UserName,
                Email = registerDTO.Email,
                PasswordHash = hashPassword,
                Salt = salt,
                CreatedAt = DateTime.UtcNow,
            };

            await _userRepository.CreateAsync(user);

            var token = _jwtProvider.GenerateToken(user);

            return new AuthResponeDTO
            {
                UserId = user.Id,
                UserName = user.Username,
                Email = user.Email,
                Expiration = user.CreatedAt,
                Token = token
            };
        }
    }
}
