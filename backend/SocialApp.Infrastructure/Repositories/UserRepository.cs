using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SocialApp.Domain.Entities;
using SocialApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserRepository(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<User>("Users");
        }

        public async Task CreateAsync(User user)
        => await _userCollection.InsertOneAsync(user);

        public async Task DeleteAsync(string id)
        => await _userCollection.DeleteOneAsync(u => u.Id == id);

        public async Task<List<User>> GetAllAsync()
        => await _userCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetByEmailAsync(string email)
        => await _userCollection.Find(u => u.Email == email).FirstOrDefaultAsync();

        public async Task<List<User>> GetByIdAsync(List<string> ids)
        {
           
            var users = await _userCollection.AsQueryable()
                                             .Where(u => ids.Contains(u.Id))
                                             .ToListAsync();

            return users;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        =>
        await _userCollection.Find(u => u.Username == username).FirstOrDefaultAsync();

        public async Task UpdateAsync(User user) =>
            await _userCollection.ReplaceOneAsync(u => u.Id == user.Id, user);


        public async Task SavePostAsync(string userId, string postId)
        {
            var user = await _userCollection.Find(u => u.Id == userId).FirstOrDefaultAsync();

            if (user != null && !user.SavedPostIds.Contains(postId))
            {
                user.SavedPostIds.Add(postId);
                await _userCollection.ReplaceOneAsync(u => u.Id == userId, user); // Cập nhật người dùng với bài viết đã lưu
            }
        }
        public async Task<List<User>> GetUsersByIdsAsync(List<string> userIds)
        {
            var filter = Builders<User>.Filter.In(u => u.Id, userIds);
            var users = await _userCollection.Find(filter).ToListAsync();
            return users;
        }

        public async Task<User> GetByIdOneAsync(string id)
        {
            return await _userCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        }
    }
}
