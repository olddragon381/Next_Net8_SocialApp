using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.DTOs
{
    public class SendRequestDto
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}
