using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialApp.Domain.Enums;

namespace SocialApp.Domain.Entities
{
    public class FriendRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string SenderId { get; set; } // ID của người gửi yêu cầu kết bạn
        public string ReceiverId { get; set; } // ID của người nhận yêu cầu kết bạn
        public DateTime SentAt { get; set; } = DateTime.UtcNow; // Thời gian gửi yêu cầu
        public FriendStatus Status { get; set; } = FriendStatus.Pending; 
        public DateTime? RejectedAt { get; set; } // Thời gian từ chối yêu cầu, null nếu chưa được từ chối
    }
}
