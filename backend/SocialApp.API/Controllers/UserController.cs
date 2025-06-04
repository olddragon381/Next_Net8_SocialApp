using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Application.DTOs;
using SocialApp.Application.Interfaces;
using SocialApp.Application.UseCase.User;
using SocialApp.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SocialApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        public AuthController(IAuthService authService,  IUserRepository userRepository, IUserService userService)
        {
            _authService = authService;
            _userRepository = userRepository;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                var result = await _authService.LoginAsync(loginDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userRepository.GetByIdAsync(new List<string> { userId });

            if (user == null || !user.Any())
                return NotFound();

            return Ok(new UserInfoDTO(user.First()));
        }


        [AllowAnonymous] 
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Append("jwt", "", new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(-1)
            });

            return Ok("Logged out");
        }
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdOne(string id)
        {
            var user = await _userService.GetUserByIdOneAsync(id);

            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }



    }
}
