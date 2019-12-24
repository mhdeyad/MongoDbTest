using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Companies.App_Start;
using Companies.Lib;
using Companies.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
//using MongoDB.Bson.IO;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Companies.Services
{
    public class CompanyService : ICompanyService
    {
        private MongoDbContext dbcontext;
        private IMongoCollection<Company> companycollection;

        public CompanyService(IConfiguration config)
        {
            dbcontext = new MongoDbContext(config);
            companycollection = dbcontext.database.GetCollection<Company>("Company");
        }
        public async Task AddColumnToCompanyCollection(string id, FieldType column)
        {


            Company company = await GetCompany(id);
            var col = new BsonDocument();
            if (column.DataType == FieldDataType.stringtype)
                col = new BsonDocument(column.Name, column.Value);

            else if (column.DataType == FieldDataType.Intype)

                col = new BsonDocument(column.Name, Convert.ToInt32(column.Value));
            else if (column.DataType == FieldDataType.Datetype)
                col = new BsonDocument(column.Name, Convert.ToDateTime(column.Value));
            if (company.Properties == null)
                company.Properties = col;
            else
                company.Properties.Add(new BsonDocument(column.Name, col));


           await UpdateCompany(id, company);
           
          
        }

        public async Task AddColumnToContactCollection(string id, FieldType column)
        {
           
            Contact contact = await GetContact(id);
            var col = new BsonDocument();
            if (column.DataType == FieldDataType.stringtype)
                col = new BsonDocument(column.Name, column.Value);

            else if (column.DataType == FieldDataType.Intype)

                col = new BsonDocument(column.Name, Convert.ToInt32(column.Value));
            else if (column.DataType == FieldDataType.Datetype)
                col = new BsonDocument(column.Name, Convert.ToDateTime(column.Value));
            if (contact.Properties == null)
                contact.Properties = col;
            else
                contact.Properties.Add(new BsonDocument(column.Name, col));


            await UpdateContact(id, contact);
        }

        public async Task<Company> AddCompany( Company company)
        {
            await dbcontext.Companies.InsertOneAsync(company);
            return company;
        }

        public async Task<Contact> AddContact(Contact contact)
        {
            await dbcontext.Contacts.InsertOneAsync(contact);
            return contact;
        }

        public async Task<IEnumerable<Company>> GetAllCompany()
        {
             List<Company> companies =await companycollection.AsQueryable<Company>().ToListAsync();
            return (companies);
        }

        public async Task<IEnumerable<Contact>> GetAllContact()
        {
            List<Contact> contacts = await dbcontext.Contacts.AsQueryable<Contact>().ToListAsync();
            return (contacts);
        }

        public async Task<IEnumerable<Company>> GetByFilter(string filterjson)
        {


            JObject JFilters = JObject.Parse(filterjson);

            var companybuldire = Builders<Company>.Filter;
              //     var companyfilert = companybuldire.AnyGte("NumberOfEmployees", 0);

            FilterDefinition<Company> companyfilert = FilterDefinition<Company>.Empty;

            foreach (var JFilter in JFilters)
            {
                companyfilert = companyfilert & companybuldire.Eq(JFilter.Key, ((Newtonsoft.Json.Linq.JValue)JFilter.Value).Value);
               
            }


            var result = await dbcontext.Companies.Find(companyfilert).ToListAsync();
            return result;
        }

        public async Task<Company> GetCompany(string CompanyId)
        {
            
            var companyfilter = Builders<Company>.Filter.Eq("ID", CompanyId);
            var result = await dbcontext.Companies.Find(companyfilter).FirstOrDefaultAsync();
            return (result);
        }

        public async Task<Contact> GetContact(string Id)
        {
            var filter = Builders<Contact>.Filter.Eq("ID", Id);
            var result = await dbcontext.Contacts.Find(filter).FirstOrDefaultAsync();
            return (result);
        }

        public async Task<DeleteResult> RemoveAll()
        {
            var result = await dbcontext.Companies.DeleteManyAsync(new BsonDocument());
            return result;
        }

        public async Task<DeleteResult> RemoveCompany(string Id)
        {
            var filter = Builders<Company>.Filter.Eq("_id", ObjectId.Parse(Id));
          var result= await  dbcontext.Companies.DeleteOneAsync(filter);
            return result;
        }

        public async Task<DeleteResult> RemoveContact(string Id)
        {
            var filter = Builders<Contact>.Filter.Eq("_id", ObjectId.Parse(Id));
            var result = await dbcontext.Contacts.DeleteOneAsync(filter);
            return result;
        }

        public async Task<Company> UpdateCompany(string id, Company company)
        {
        
           
            var filter = Builders<Company>.Filter.Eq("_id", ObjectId.Parse(id));
           await  dbcontext.Companies.ReplaceOneAsync(filter, company);

            return company;

        }

        public async Task<Contact> UpdateContact(string id, Contact contact)
        {
            var filter = Builders<Contact>.Filter.Eq("_id", ObjectId.Parse(id));
            await dbcontext.Contacts.ReplaceOneAsync(filter, contact);

            return contact;
        }
    }
}