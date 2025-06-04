using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.DTOs
{
    public class FriendRequestNotificationDTO
    {
   

        public string UserId { get; set; }
        public string Title { get; set; } = "Bạn có lời mời kết bạn";
        public string Content { get; set; } = " đã gửi lời mời kết bạn";
        public bool IsRead { get; set; } = false; // Mặc định là chưa đọc
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
