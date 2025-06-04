using MongoDB.Driver;
using SocialApp.Domain.Entities;
using SocialApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Infrastructure.Repositories
{
    public class SavedPostRespository : ISavedPostRepository
    {
        private readonly IMongoCollection<SavePost> _savedpostcollection;
        private readonly IMongoCollection<Post> _postcollection;

        public SavedPostRespository(IMongoDatabase database)
        {
            _savedpostcollection = database.GetCollection<SavePost>("SavedPosts");
            _postcollection = database.GetCollection<Post>("Posts");
        }



        public async Task AddSavedPostAsync(string userId, string postId)
        {
            var existingSavedPost = await _savedpostcollection.Find(sp => sp.UserId == userId && sp.PostId == postId).AnyAsync();
            if (!existingSavedPost)
            {
                var savedPost = new SavePost { UserId = userId, PostId = postId };
                await _savedpostcollection.InsertOneAsync(savedPost);

            }
        }

        public async Task<List<Post>> GetSavedPostIdsByUserAsync(string userId)
        {
            var savedPosts = await _savedpostcollection.Find(sp => sp.UserId == userId).ToListAsync();
            var postIds = savedPosts.Select(sp => sp.PostId).ToList();
            var posts = await _postcollection.Find(p => postIds.Contains(p.Id)).ToListAsync();
            return posts;
        }

        public async Task<bool> IsPostSavedByUserAsync(string userId, string postId)
        {
            return await _savedpostcollection.Find(sp => sp.UserId == userId && sp.PostId == postId).AnyAsync();
        }

        public async Task<SavePost?> GetByUserAndPostIdAsync(string userId, string postId)
        {
            return await _savedpostcollection.Find(sp => sp.UserId == userId && sp.PostId == postId)
                                    .FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(string id)
        {
            await _savedpostcollection.DeleteOneAsync(sp => sp.Id == id);
        }
    }
}
