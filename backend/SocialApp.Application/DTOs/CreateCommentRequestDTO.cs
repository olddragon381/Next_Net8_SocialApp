using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.DTOs
{
    public class CreateCommentRequestDTO
    {
        [Required]
        public string Content { get; set; }
    }
}
