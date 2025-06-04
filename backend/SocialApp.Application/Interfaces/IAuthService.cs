using SocialApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponeDTO> LoginAsync(LoginDTO loginDTO);
        Task<AuthResponeDTO> RegisterAsync(RegisterDTO registerDTO);
    }
}
