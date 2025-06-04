using SocialApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetByIdAsync(List<string> ids);
       
        Task<User?> GetByUsernameAsync(string username);
        Task SavePostAsync(string userId, string postId);
        Task<User?> GetByEmailAsync(string email);
        Task<List<User>> GetAllAsync();

        Task CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);


        Task<List<User>> GetUsersByIdsAsync(List<string> userIds);
        Task<User> GetByIdOneAsync(string id);

    }
}
