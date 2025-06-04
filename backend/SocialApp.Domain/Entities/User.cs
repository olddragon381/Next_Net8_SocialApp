using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Domain.Entities
{
    public class User
    {


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 

        public string Username { get; set; }
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

      
        public List<string> SavedPostIds { get; set; } = new List<string>();

    }
}
