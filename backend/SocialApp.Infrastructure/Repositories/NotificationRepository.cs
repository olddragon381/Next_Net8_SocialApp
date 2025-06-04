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
    public class NotificationRepository : INotificationRepository
    {
        private readonly IMongoCollection<Notification> _collectionNotification;

        public NotificationRepository(IMongoDatabase database)
        {
            _collectionNotification = database.GetCollection<Notification>("Notifications");
        }

        public Task AddAsync(Notification notification)
        =>  _collectionNotification.InsertOneAsync(notification);
        

        public Task<List<Notification>> GetByUserIdAsync(string userId)
        => _collectionNotification
            .Find(n => n.UserId == userId)
            .SortByDescending(n => n.CreatedAt)
            .ToListAsync();

        public Task MarkAsReadAsync(string notificationId)
        {
            var update = Builders<Notification>.Update.Set(n => n.IsRead, true);
            return _collectionNotification.UpdateOneAsync(n => n.Id == notificationId, update);
        }
     }
}
