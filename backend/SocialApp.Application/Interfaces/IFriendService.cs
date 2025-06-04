using SocialApp.Domain.Entities;
using SocialApp.Domain.Enums;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialApp.Application.DTOs;

namespace SocialApp.Application.Interfaces
{
    public interface IFriendService
    {
        Task<Result> SendRequestAsync(string senderId, string receiverId);
        Task<Result> AcceptRequestAsync(string requestId);
        Task<Result> RejectRequestAsync(string requestId);
        Task<List<FriendRequest>> GetReceivedRequests(string userId);
        Task<List<FriendRequest>> GetSentRequests(string userId);
        Task<List<UserInfoDTO>> GetFriendsAsync(string userId);
        Task<Result<string>> UnfriendAsync(string userId1, string userId2);
    }
}
