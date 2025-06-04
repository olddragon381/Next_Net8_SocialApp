using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options; // cần thiết cho .Configure()
using MongoDB.Driver;

using SocialApp.Application.Interfaces;
using SocialApp.Application.Settings;
using SocialApp.Application.UseCase.Friend;
using SocialApp.Domain.Interfaces;
using SocialApp.Infrastructure.Conf;

using SocialApp.Infrastructure.Repositories;
using SocialApp.Infrastructure.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<JwtSettings>(config.GetSection("JwtSettings"));
        services.AddScoped<IJwtProvider, JwtProvider>();


        // Fix: Use Bind instead of passing IConfigurationSection directly to Configure
        var mongoDbSettingsSection = config.GetSection("MongoDbSettings");
        services.Configure<MongoDbSettings>(mongoDbSettingsSection.Bind);

        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        services.AddScoped<IMongoDatabase>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName);
        });

        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ISavedPostRepository, SavedPostRespository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();
        
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        return services;
    }
}
