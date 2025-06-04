using Microsoft.AspNetCore.SignalR;
using SocialApp.Application.DTOs;
using SocialApp.Application.Interfaces;
using SocialApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.UseCase.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepo;
        private readonly IUserRepository _userRepo;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(INotificationRepository notificationRepo, IUserRepository userRepo)
        {
            _notificationRepo = notificationRepo;
            _userRepo = userRepo;
        }

        public async Task CreateFriendRequestNotification(string requestId ,string receiverUserId, string senderUserName)
        {
            var username = _userRepo.GetByIdOneAsync(receiverUserId).Result?.Username;

            // Map FriendRequestNotificationDTO to Notification entity
            var notification = new Domain.Entities.Notification
            {
                RequestId = requestId,
                UserId = receiverUserId,
                Type = 0,
                Title = "Lời mời kết bạn",
                Content = $"{username} đã gửi cho bạn một lời mời kết bạn.",
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
            };

            await _notificationRepo.AddAsync(notification);
            var connectionId = NotificationHub.GetConnectionId(receiverUserId);
            if (connectionId != null)
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", notification);
            }
        }

        public async Task CreateLikeRequestNotification(string requestId, string receiverUserId, string senderUserName)
        {
            var username = _userRepo.GetByIdOneAsync(receiverUserId).Result?.Username;

            // Map FriendRequestNotificationDTO to Notification entity
            var notification = new Domain.Entities.Notification
            {
                RequestId = requestId,
                UserId = receiverUserId,
                Type = Domain.Enums.NoficationType.LikeRequest,
                Title = "Like bài viết",
                Content = $"{username} đã Like bài viết của bạn",
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
            };

            await _notificationRepo.AddAsync(notification);
            var connectionId = NotificationHub.GetConnectionId(receiverUserId);
            if (connectionId != null)
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", notification);
            }
        }

        public async  Task CreatePostRequestNotification(string requestId, List<string> receiverUserId, string senderUserName)
        {
            for (int i = 0; i < receiverUserId.Count; i++)
            {
                

                var username = _userRepo.GetByIdOneAsync(receiverUserId[i]).Result?.Username;

                // Map FriendRequestNotificationDTO to Notification entity
                var notification = new Domain.Entities.Notification
                {
                    RequestId = requestId,
                    UserId = receiverUserId[i],
                    Type = Domain.Enums.NoficationType.PostRequest,
                    Title = "Bạn của bạn Post bài",
                    Content = $"{username} Post bài viết",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow,
                };

                await _notificationRepo.AddAsync(notification);
                var connectionId = NotificationHub.GetConnectionId(receiverUserId[i]);
                if (connectionId != null)
                {
                    await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", notification);
                }

            }
   
        }

        public Task<List<Domain.Entities.Notification>> GetUserNotifications(string userId)
        =>_notificationRepo.GetByUserIdAsync(userId);

        public Task MarkNotificationAsRead(string notificationId)
        =>_notificationRepo.MarkAsReadAsync(notificationId);
    }
}
