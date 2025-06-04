using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialApp.Application.DTOs;
using SocialApp.Application.Interfaces;
using SocialApp.Domain.Interfaces;

namespace SocialApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly ICommentRepository _commentRepository;

        public CommentsController(ICommentService commentService, ICommentRepository commentRepository)
        {
            _commentService = commentService;
            _commentRepository = commentRepository;
        }

        [Authorize]
        [HttpPost("{postId}")]
        public async Task<IActionResult> AddComment(string postId, [FromBody] CreateCommentRequestDTO content)
        {
            var userId = User.FindFirst("userId")?.Value;
            if (userId == null) return Unauthorized();

            await _commentService.AddCommentAsync(postId, userId, content.Content);
            return Ok(new { message = "Comment added successfully." });
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetComments(string postId)
        {
            var comments = await _commentService.GetCommentsWithUsernameAsync(postId);

            return Ok(comments);
        }
        [HttpGet("{postId}/count")]
        public async Task<IActionResult> GetCommentCount(string postId)
        {
            var count = await _commentService.CountCommentsByPostIdAsync(postId);
            return Ok(new { count });
        }

    }
}
