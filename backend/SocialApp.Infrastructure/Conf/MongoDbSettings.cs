﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Infrastructure.Conf
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
      
    }
}
