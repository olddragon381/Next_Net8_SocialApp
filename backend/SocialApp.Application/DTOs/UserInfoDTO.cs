using SocialApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.DTOs
{
    public class UserInfoDTO
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        // Có thể thêm Avatar, Role, ... nếu cần

        public UserInfoDTO() { }

        public UserInfoDTO(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
        }
    }
}
