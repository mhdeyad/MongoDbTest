using Companies.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Companies.App_Start
{
    public class MongoDbContext
    {
       
        public IMongoDatabase database;
        public MongoDbContext(IConfiguration config)
        {
            var mongoclient = new MongoClient(config.GetSection("DataBaseSettings").GetSection("ConnectionString").Value);
            database = mongoclient.GetDatabase("Companies");
        }

        public IMongoCollection<Company> Companies
        {
            get
            {
                return database.GetCollection<Company>("Company");
            }

        }
        public IMongoCollection<Contact> Contacts
        {
            get
            {
                return database.GetCollection<Contact>("Contact");
            }

        }

    }
}
