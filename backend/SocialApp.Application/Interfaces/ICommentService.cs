using SocialApp.Application.DTOs;
using SocialApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.Interfaces
{
    public interface ICommentService
    {
        Task AddCommentAsync(string postId, string userId, string content);
        Task<List<CommentDto>> GetCommentsWithUsernameAsync(string postId);
        Task<int> CountCommentsByPostIdAsync(string postId);


    }   
}
