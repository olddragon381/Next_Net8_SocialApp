using MongoDB.Bson;
using MongoDB.Driver;
using SocialApp.Domain.Entities;
using SocialApp.Domain.Interfaces;
using SocialApp.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IMongoCollection<Post> _postsCollection;

        public PostRepository(IMongoDatabase database)
        {
            _postsCollection = database.GetCollection<Post>("Posts");
        }

        public async Task<string> CreateAsync(Post post)
        {
            await _postsCollection.InsertOneAsync(post);
            return post.Id;
        }

        public async Task DeleteAsync(string postId, string userId)
        {
            var post = await _postsCollection.Find(p => p.Id == postId).FirstOrDefaultAsync();

            if (post != null)
            {
                post.IsDeleted = true;
              
                post.DeletedAt = DateTime.UtcNow;

                await _postsCollection.ReplaceOneAsync(p => p.Id == postId, post);
            }
        }
        public async Task<List<Post>> GetAllAsync()
        {
            return await _postsCollection.Find(p => !p.IsDeleted).SortByDescending(p => p.CreatedAt).ToListAsync();
        }


        public async Task<List<Post>> GetPostsByIdsAsync(List<string> postIds)
        {
            return await _postsCollection.Find(p => postIds.Contains(p.Id)).ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(string id)
        {
            
            if (string.IsNullOrWhiteSpace(id) || !ObjectId.TryParse(id, out _))
            {
                throw new ArgumentException("ID không hợp lệ. Phải là 24 ký tự hex.");
            }

            return await _postsCollection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Post post)
        {
            await _postsCollection.ReplaceOneAsync(p => p.Id == post.Id, post);
        }

        public async Task<List<Post>> GetPostByUserIdAsync(string userId)
        {
            return await _postsCollection.Find(p => p.UserId == userId).SortByDescending(p => p.CreatedAt).ToListAsync();
        }
    }

}
