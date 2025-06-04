using MongoDB.Driver;
using SocialApp.Domain.Entities;
using SocialApp.Domain.Enums;
using SocialApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Infrastructure.Repositories
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly IMongoCollection<FriendRequest> _friendRequestColection;

        public FriendRequestRepository(IMongoDatabase database)
        {
            _friendRequestColection = database.GetCollection<FriendRequest>("FriendRequest");
        }


        public Task AddAsync(FriendRequest request)
        {
            return _friendRequestColection.InsertOneAsync(request);
        }

        public Task<FriendRequest> GetPendingRequest(string senderId, string receiverId)
        {
            return _friendRequestColection
                .Find(r => r.SenderId == senderId && r.ReceiverId == receiverId)
                .FirstOrDefaultAsync();
        }

        public Task<List<FriendRequest>> GetReceivedRequests(string userId)
        {
            return _friendRequestColection
                .Find(r => r.ReceiverId == userId && r.Status == FriendStatus.Pending)
                .SortByDescending(r => r.RejectedAt)
                .ToListAsync();
        }

        public Task<FriendRequest> GetRequestBetweenUsers(string userId1, string userId2)
        {
            return _friendRequestColection
                .Find(r =>
                    (r.SenderId == userId1 && r.ReceiverId == userId2) ||
                    (r.SenderId == userId2 && r.ReceiverId == userId1)
                )
                .FirstOrDefaultAsync();
        }

        public Task<List<FriendRequest>> GetSentRequests(string userId)
        {
            return _friendRequestColection
                .Find(r => r.SenderId == userId && r.Status == FriendStatus.Pending)
                .SortByDescending(r => r.SentAt)
                .ToListAsync();
        }

        public Task UpdateStatusAsync(string requestId, FriendStatus status)
        {
            var update = Builders<FriendRequest>.Update.Set(r => r.Status, status);
            Console.WriteLine(update);

            return _friendRequestColection
                .UpdateOneAsync(r => r.Id == requestId, update);
        }

        public async Task<List<string>> GetFriends(string userId)
        {
            var filter = Builders<FriendRequest>.Filter.And(
                Builders<FriendRequest>.Filter.Eq(r => r.Status, FriendStatus.Accepted),
                Builders<FriendRequest>.Filter.Or(
                    Builders<FriendRequest>.Filter.Eq(r => r.SenderId, userId),
                    Builders<FriendRequest>.Filter.Eq(r => r.ReceiverId, userId)
                )
            );

            var requests = await _friendRequestColection.Find(filter).ToListAsync();

            var friends = requests.Select(r =>
                r.SenderId == userId ? r.ReceiverId : r.SenderId
            ).ToList();

            return friends;
        }

        public async Task<bool> UnfriendAsync(string userId1, string userId2)
        {
            var filter = Builders<FriendRequest>.Filter.And(
        Builders<FriendRequest>.Filter.Or(
            Builders<FriendRequest>.Filter.And(
                Builders<FriendRequest>.Filter.Eq(r => r.SenderId, userId1),
                Builders<FriendRequest>.Filter.Eq(r => r.ReceiverId, userId2)
            ),
            Builders<FriendRequest>.Filter.And(
                Builders<FriendRequest>.Filter.Eq(r => r.SenderId, userId2),
                Builders<FriendRequest>.Filter.Eq(r => r.ReceiverId, userId1)
            )
        ),
        Builders<FriendRequest>.Filter.Eq(r => r.Status, FriendStatus.Accepted)
    );

            var update = Builders<FriendRequest>.Update
        .Set(r => r.Status, FriendStatus.Pending)
        .Set(r => r.RejectedAt, DateTime.UtcNow); // nếu bạn có cột thời gian

            var result = await _friendRequestColection.UpdateOneAsync(filter, update);

            return result.ModifiedCount > 0;

            
        }
    }
}
