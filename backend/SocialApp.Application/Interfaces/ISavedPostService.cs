using SocialApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application.Interfaces
{
    public interface ISavedPostService
    {
        Task AddSavePostAsync(string userId, string postId);

        //Task<bool> IsPostSavedByUserAsync(string userId, string postId);

        Task<List<PostDTO>> GetSavedPostIdsByUserAsync(string userId);

        Task<bool> UnsavePostAsync(string userId, string postId);



    }
}
