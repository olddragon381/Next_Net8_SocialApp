using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.DTOs
{
    public class CreatePostDTO
    {
        [Required]
        public string Content { get; set; }
        public string UserId { get; set; }
    }
}
