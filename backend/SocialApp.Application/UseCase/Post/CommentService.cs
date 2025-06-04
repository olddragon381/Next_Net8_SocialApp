using SocialApp.Application.DTOs;
using SocialApp.Application.Interfaces;
using SocialApp.Domain.Entities;
using SocialApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.UseCase.Post
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        public async Task AddCommentAsync(string postId, string userId, string content)
        {
            var comment = new Comment
            {
                PostId = postId,
                UserId = userId,
                Content = content
            };

            await _commentRepository.AddCommentAsync(comment);
        }
        
        public async Task<List<Comment>> GetCommentsByPostIdAsync(string postId)
        {
            return await _commentRepository.GetCommentsByPostIdAsync(postId);
        }
        public async Task<int> CountCommentsByPostIdAsync(string postId)
        {
            return await _commentRepository.CountCommentsByPostIdAsync(postId);
        }

        public async Task<List<CommentDto>> GetCommentsWithUsernameAsync(string postId)
        {
            var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);

            var userIds = comments.Select(c => c.UserId).Distinct().ToList();

            var users = await _userRepository.GetUsersByIdsAsync(userIds);

            var commentDtos = comments.Select(c =>
            {
                var user = users.FirstOrDefault(u => u.Id == c.UserId);
                return new CommentDto
                {
                    Id = c.Id,
                    PostId = c.PostId,
                    UserId = c.UserId,
                    Username = user?.Username ?? "Unknown",
                    Content = c.Content,
                    CommentedAt = c.CommentedAt
                };
            }).ToList();

            return commentDtos;
        }
    }
}
