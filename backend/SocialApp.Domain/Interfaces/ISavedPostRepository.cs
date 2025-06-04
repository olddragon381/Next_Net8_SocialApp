using SocialApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Domain.Interfaces
{
    public interface ISavedPostRepository
    {
        Task AddSavedPostAsync(string userId, string postId);
       
        Task<bool> IsPostSavedByUserAsync(string userId, string postId);
        Task<List<Post>> GetSavedPostIdsByUserAsync(string userId);
        Task<SavePost?> GetByUserAndPostIdAsync(string userId, string postId);
        Task DeleteAsync(string id);

    }
}
