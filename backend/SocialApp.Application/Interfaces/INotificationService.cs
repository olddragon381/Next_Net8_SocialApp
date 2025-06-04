using SocialApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.Interfaces
{
    public interface INotificationService
    {
        Task CreateFriendRequestNotification(string requestId, string receiverUserId, string senderUserName);
        Task CreateLikeRequestNotification(string requestId, string receiverUserId, string senderUserName);
        Task CreatePostRequestNotification(string requestId, List<string> receiverUserId, string senderUserName);
        Task<List<Notification>> GetUserNotifications(string userId);
        Task MarkNotificationAsRead(string notificationId);
    }
}
