using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.DTOs
{
    public class PostWithUserDTO
    {
        public string Id { get; set; }        // Id của bài viết
        public string UserId { get; set; }    // Người tạo bài viết
        public string UserName { get; set; }  // Tên người tạo bài viết
        public string Content { get; set; }   // Nội dung bài viết
        public DateTime CreatedAt { get; set; } // Ngày tạo bài viết
        public int LikesCount { get; set; }   // Số lượt like

        // Thêm thông tin về bài viết đã bị xóa hay chưa
        public bool IsDeleted { get; set; }

        // Thêm thông tin người xóa (nếu có)
        public string DeletedBy { get; set; }

        // Thêm thời gian xóa (nếu có)
        public DateTime? DeletedAt { get; set; }
    }
    }
