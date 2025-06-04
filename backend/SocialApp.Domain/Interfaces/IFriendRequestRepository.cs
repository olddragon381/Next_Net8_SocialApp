using SocialApp.Domain.Entities;
using SocialApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Domain.Interfaces
{
    public interface IFriendRequestRepository
    {
        Task AddAsync(FriendRequest request);
        Task<FriendRequest> GetPendingRequest(string senderId, string receiverId);
        Task<List<FriendRequest>> GetReceivedRequests(string userId);
        Task<List<FriendRequest>> GetSentRequests(string userId);
        Task UpdateStatusAsync(string requestId, FriendStatus status);
        Task<FriendRequest> GetRequestBetweenUsers(string userId1, string userId2);

        Task<List<string>> GetFriends(string userId);

        Task<bool> UnfriendAsync(string userId1, string userId2);
    }
}
