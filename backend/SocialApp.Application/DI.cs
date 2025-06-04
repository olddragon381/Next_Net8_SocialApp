using Microsoft.Extensions.DependencyInjection;
using SocialApp.Application.Interfaces;
using SocialApp.Application.UseCase.Auth;
using SocialApp.Application.UseCase.Friend;
using SocialApp.Application.UseCase.Notification;
using SocialApp.Application.UseCase.Post;
using SocialApp.Application.UseCase.SavedPost;
using SocialApp.Application.UseCase.User;
using SocialApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Application
{
    public static class DependencyInjectionExtensions 
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            services.AddScoped<IPostService, PostService>(); 
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ISavedPostService, SavedPostService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<NotificationService>();
            return services;
        }
    }
}
