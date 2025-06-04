using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.DTOs
{
    public class CommentDto
    {
        public string Id { get; set; }
        public string PostId { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; } 
        public string Content { get; set; }
        public DateTime CommentedAt { get; set; }
    }

}
