using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Domain.Entities
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> LikeUserIds { get; set; } = new();

        public bool IsDeleted { get; set; } = false;

        public DateTime? DeletedAt { get; set; }

        [BsonIgnore]
        public int LikesCount => LikeUserIds.Count;

        public Post(string userId, string content)
        {
           
            UserId = userId;
            Content = content;
            CreatedAt = DateTime.UtcNow;
            LikeUserIds = new();
            IsDeleted = false;
            DeletedAt = null;
        }

        // Fix for CS0501: Provide a body for the parameterless constructor  
        public Post()
        {
           
            UserId = string.Empty;
            Content = string.Empty;
            CreatedAt = DateTime.UtcNow;
            LikeUserIds = new();
            IsDeleted = false;
            DeletedAt = null;
        }
    }
}
