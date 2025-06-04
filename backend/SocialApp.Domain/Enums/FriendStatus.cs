using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Domain.Enums
{
    public enum FriendStatus
    {
        Pending,    // Friend request sent, waiting for approval
        Accepted,   // Friend request accepted
        Rejected,   // Friend request rejected
    }
}
