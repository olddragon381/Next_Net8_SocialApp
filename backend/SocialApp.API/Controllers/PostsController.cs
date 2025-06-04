using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Application.DTOs;
using SocialApp.Application.Interfaces;
using SocialApp.Domain.Interfaces;
using SocialApp.Infrastructure.Repositories;
using System.Security.Claims;

namespace SocialApp.API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostsController(IPostService postService, IPostRepository postRepository, IUserRepository userRepository)
        {
            _postService = postService;
            _postRepository = postRepository;
            _userRepository = userRepository;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDTO dto)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine(userId);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Không có thông tin người dùng.");
            }

            dto.UserId = userId;
            dto.Content = dto.Content;
            // Tạo bài viết
            var postId = await _postService.CreatePostAsync(dto);

            return CreatedAtAction(nameof(CreatePost), new { Id = postId });
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPostById(string postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);

            if (post == null || post.IsDeleted)
                return NotFound("Post not found or it has been deleted");

            var postDto = new PostDTO(post); // Chuyển Entity thành DTO
            return Ok(postDto);
        }
        [Authorize]
        [HttpPost("{postId}/like")]
        public async Task<IActionResult> LikePost(string postId)
        {

            var userId = User.FindFirst("userId")?.Value
          ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token");
            }




            await _postService.LikePostAsync(postId, userId);
            return Ok(new { message = "Like/Unlike successful!" });
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetAllAsync();

            // Lọc bài viết không bị xóa
            var activePosts = posts.Where(post => !post.IsDeleted).ToList();

            // Lấy danh sách UserId từ các bài viết
            var userIds = activePosts
    .Where(p => !string.IsNullOrEmpty(p.UserId)) // <-- THÊM DÒNG NÀY
    .Select(p => p.UserId)
    .Distinct()
    .ToList();


            var users = await _userRepository.GetByIdAsync(userIds);

            if (users == null)
            {
                return StatusCode(500, "Không thể lấy danh sách người dùng.");
            }

            // Tạo danh sách DTO trả về
            var postDtos = activePosts.Select(post =>
            {
                var user = users.FirstOrDefault(u => u.Id == post.UserId);

                return new PostWithUserDTO
                {
                    Id = post.Id.ToString(),
                    Content = post.Content,
                    CreatedAt = post.CreatedAt,
                    UserId = post.UserId,
                    UserName = user?.Username ?? "Unknown",
                    DeletedAt = post.DeletedAt,
                    IsDeleted = post.IsDeleted,
                    LikesCount = post.LikesCount,
                };
            }).ToList();

            return Ok(postDtos);
        }
        [Authorize]
        [HttpDelete("{postId}")]
        public async Task<IActionResult> SoftDeletePost(string postId)
        {
            // Lấy userId từ JWT token
            var userId = HttpContext.User.Claims.First(x => x.Type == "userId").Value;

            try
            {
                await _postService.DeletePostAsync(postId, userId);
                return Ok(new { message = "Post successfully deleted!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize]
        [HttpPost("save/{postId}")]
        public async Task<IActionResult> SavePost(string postId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy userId từ JWT token
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated");
            }

            await _postService.SavePost(postId);
            return Ok("Post saved successfully");
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPostsByUserId(string userId)
        {
            var posts = await _postService.GetPostsByUserIdAsync(userId);
            return Ok(posts);
        }

    }
}
