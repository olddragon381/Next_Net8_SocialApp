using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.Interfaces
{
    public interface ProtectAuth
    {
        string generateSalth();
        string hashPassword(string password, string salt);
    }
}
