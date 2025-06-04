using SocialApp.Application.DTOs;
using SocialApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.Interfaces
{
    public interface IPostService
    {
        Task<string> CreatePostAsync(CreatePostDTO createPostDTO);

        Task LikePostAsync(string postId, string userId);

        Task DeletePostAsync(string postId, string userId);

        Task SavePost(string postId);

        Task<List<Post>> GetPostsByUserIdAsync(string userId);
    }
}
