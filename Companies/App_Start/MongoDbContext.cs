using Companies.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Companies.App_Start
{
    public class MongoDbContext
    {
        MongoClient client;
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
public IMongoCollection<User> Users 
{
            get
            {
                return database.GetCollection<User>("User");
            }
            
        }
    }
}
