using SocialApp.Application.DTOs;
using SocialApp.Application.Interfaces;
using SocialApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.UseCase.SavedPost
{
    public class SavedPostService : ISavedPostService
    {
        private readonly ISavedPostRepository _repo;

        public SavedPostService(ISavedPostRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<PostDTO>> GetSavedPostIdsByUserAsync(string userId)
        {
            var posts = await _repo.GetSavedPostIdsByUserAsync(userId);

            // Map from Post → PostDTO
            var postDtos = posts.Select(p => new PostDTO
            {
                Id = p.Id,
                UserId = p.UserId,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                LikesCount = p.LikesCount,
                IsDeleted = p.IsDeleted,
                DeletedBy = "",
                DeletedAt = p.DeletedAt
            }).ToList();

            return postDtos;
        }

        public async Task AddSavePostAsync(string userId, string postId)
        {
            await _repo.AddSavedPostAsync(userId, postId);
        }

        public async Task<bool> UnsavePostAsync(string userId, string postId)
        {
            var savedPost = await _repo.GetByUserAndPostIdAsync(userId, postId);
            if (savedPost == null) return false;

            await _repo.DeleteAsync(savedPost.Id);
            return true;
        }
    }
}
