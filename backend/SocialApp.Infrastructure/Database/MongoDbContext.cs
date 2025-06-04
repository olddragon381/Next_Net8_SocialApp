using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SocialApp.Domain.Entities;
using SocialApp.Infrastructure.Conf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Infrastructure.Database
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> mongoDbSettings)
        {
            // Sử dụng chuỗi kết nối từ MongoDbSettings để kết nối với MongoDB Atlas
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            _database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        }

        // Lấy collection "Posts"
        public IMongoDatabase Database => _database;
    }
}
