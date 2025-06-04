using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Application.Interfaces;
using SocialApp.Application.UseCase.SavedPost;
using System.Security.Claims;

namespace SocialApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SavedPostsController : ControllerBase
    {
        private readonly ISavedPostService _service;

        public SavedPostsController(ISavedPostService service)
        {
            _service = service;
        }
        [Authorize]
        [HttpPost("{postId}")]
        public async Task<IActionResult> SavePost(string postId)
        {
            // giả sử userId lấy từ JWT
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            await _service.AddSavePostAsync(userId, postId);
            return Ok();
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetSavedPosts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var posts = await _service.GetSavedPostIdsByUserAsync(userId);
            return Ok(posts);
        }

        [HttpDelete("{postId}")]
        [Authorize]
        public async Task<IActionResult> DeleteSavedPost(string postId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var success = await _service.UnsavePostAsync(userId, postId);
            if (!success)
                return NotFound("Không tìm thấy bài viết đã lưu.");

            return NoContent(); // 204
        }
    }

}
