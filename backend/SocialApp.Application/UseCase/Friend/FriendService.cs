using CSharpFunctionalExtensions;
using SocialApp.Application.DTOs;
using SocialApp.Application.Interfaces;
using SocialApp.Application.UseCase.Notification;
using SocialApp.Domain.Entities;
using SocialApp.Domain.Enums;
using SocialApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.UseCase.Friend
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRequestRepository _repository;
        private readonly NotificationService _notificationService;
        private readonly IUserRepository _userRepository;   
        public FriendService(IFriendRequestRepository repository, NotificationService _notificationService, IUserRepository userRepository )
        {
            _repository = repository;
            this._notificationService = _notificationService;
            _userRepository = userRepository;
        }
        public async Task<Result> SendRequestAsync(string senderId, string receiverId)
        {
            if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(receiverId))
            {
                return Result.Failure("Sender or receiver ID cannot be null or empty.");
            }
            if (senderId == receiverId)
            {
                return Result.Failure("Không thể kết bạn với chính mình");
            }
            var existing = await _repository.GetRequestBetweenUsers(senderId, receiverId);

            if (existing != null)
            {
                if (existing.Status == FriendStatus.Accepted)
                    return Result.Failure("Hai bạn đã là bạn bè.");

                if (existing.SenderId == receiverId && existing.ReceiverId == senderId && existing.Status == FriendStatus.Pending)
                {
                    // Người nhận gửi trước → auto accept
                    existing.Status = FriendStatus.Accepted;
                    await _repository.UpdateStatusAsync(existing.Id, FriendStatus.Accepted);
                    return Result.Success("Đã chấp nhận lời mời kết bạn từ đối phương.");
                }

                return Result.Failure("Yêu cầu kết bạn đã tồn tại hoặc đang chờ xử lý.");
            }
            var request = new FriendRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Status = FriendStatus.Pending
            };
            await _repository.AddAsync(request);
            await _notificationService.CreateFriendRequestNotification(request.Id,receiverId, senderId);
            return Result.Success("Yêu cầu kết bạn đã được gửi thành công.");

        }
        public async Task<Result> AcceptRequestAsync(string requestId)
        {
            await _repository.UpdateStatusAsync(requestId, FriendStatus.Accepted);
            return Result.Success("Yêu cầu kết bạn đã được chấp nhận.");
        }

   

        public async Task<Result> RejectRequestAsync(string requestId)
        {
            await _repository.UpdateStatusAsync(requestId, FriendStatus.Rejected);
            return Result.Success("Yêu cầu kết bạn đã bị từ chối.");

        }

        public async Task<List<FriendRequest>> GetReceivedRequests(string userId)
        {
            return await _repository.GetReceivedRequests(userId);
        }

        public Task<List<FriendRequest>> GetSentRequests(string userId)
        {
            return _repository.GetSentRequests(userId);
        }

        public async Task<List<UserInfoDTO>> GetFriendsAsync(string userId)
        {
            var friendIds = await _repository.GetFriends(userId);

            var users = await _userRepository.GetUsersByIdsAsync(friendIds); 

            return users.Select(u => new UserInfoDTO
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
            }).ToList();
        }

        public async Task<Result<string>> UnfriendAsync(string userId1, string userId2)
        {
            var existing = await _repository.GetRequestBetweenUsers(userId1, userId2); // Await the task to get the actual FriendRequest object.
            if (existing == null || existing.Status != FriendStatus.Accepted) // Compare the Status property of the FriendRequest object.
                return Result.Failure<string>("Không tìm thấy mối quan hệ bạn bè."); // Ensure the Result type matches Result<string>.

            var success = await _repository.UnfriendAsync(userId1, userId2);

            if (!success)
                return Result.Failure<string>("Hai người không phải là bạn bè."); // Ensure the Result type matches Result<string>.

            return Result.Success("Đã hủy kết bạn thành công."); // Ensure the Result type matches Result<string>.
        }
    }
}
