using SocialApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(string id);
        Task<List<Post>> GetAllAsync();
        Task UpdateAsync(Post post);

        Task<string> CreateAsync(Post post);
      
        Task DeleteAsync(string postId, string userId);

        Task<List<Post>> GetPostsByIdsAsync(List<string> postIds);

        Task<List<Post>> GetPostByUserIdAsync(string userId);

    }
}
