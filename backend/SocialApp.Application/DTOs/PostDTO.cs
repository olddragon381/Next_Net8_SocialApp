using SocialApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SocialApp.Application.DTOs
{
    public class PostDTO
    {
        public string Id { get; set; }        // Id của bài viết
        public string UserId { get; set; }    // Người tạo bài viết
        public string Content { get; set; }   // Nội dung bài viết
        public DateTime CreatedAt { get; set; } // Ngày tạo bài viết
        public int LikesCount { get; set; }   // Số lượt like

        // Thêm thông tin về bài viết đã bị xóa hay chưa
        public bool IsDeleted { get; set; }

        // Thêm thông tin người xóa (nếu có)
        public string DeletedBy { get; set; }

        // Thêm thời gian xóa (nếu có)
        public DateTime? DeletedAt { get; set; }

        // Constructor để khởi tạo PostDTO từ Post Entity
        
        public PostDTO(Post post)
        {
            Id = post.Id;
            UserId = post.UserId;
            Content = post.Content;
            CreatedAt = post.CreatedAt;
            LikesCount = post.LikesCount;
            IsDeleted = post.IsDeleted;
           
            DeletedAt = post.DeletedAt;
        }
        public PostDTO() { }

        [JsonConstructor]
        public PostDTO(string id, string userId, string content, DateTime createdAt, int likesCount, bool isDeleted, string deletedBy, DateTime? deletedAt)
        {
            Id = id;
            UserId = userId;
            Content = content;
            CreatedAt = createdAt;
            LikesCount = likesCount;
            IsDeleted = isDeleted;
            
            DeletedAt = deletedAt;
        }
    }
}
