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
    public class CommentRepository : ICommentRepository
    {
        private readonly IMongoCollection<Comment> _commentsCollection;

        public CommentRepository(IMongoDatabase database)
        {
            _commentsCollection = database.GetCollection<Comment>("Comments");
        }

        public async Task AddCommentAsync(Comment comment)
        {
            await _commentsCollection.InsertOneAsync(comment);
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(string postId)
        {
            return await _commentsCollection
                .Find(c => c.PostId == postId)
                .SortByDescending(c => c.CommentedAt)
                .ToListAsync();
        }
        public async Task<int> CountCommentsByPostIdAsync(string postId)
        {
            return (int)await _commentsCollection.CountDocumentsAsync(c => c.PostId == postId);
        }
    }
}
