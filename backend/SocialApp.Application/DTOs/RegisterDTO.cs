﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.DTOs
{
    public class RegisterDTO
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }

    
    }
}
