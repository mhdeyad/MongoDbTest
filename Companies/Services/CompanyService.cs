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

        public CompanyService(IConfiguration config)
        {
            dbcontext = new MongoDbContext(config);
        }
        public async Task AddPropertyToCompanyDocumentAsync(string id, PropertyType column)
        {
            Company company = await GetCompanyAsync(id);
            var col = new BsonDocument();
            if (column.DataType == PropertyDataType.stringtype)
                col = new BsonDocument(column.Name, column.Value);
            else if (column.DataType == PropertyDataType.Intype)
                col = new BsonDocument(column.Name, Convert.ToInt32(column.Value));
            else if (column.DataType == PropertyDataType.Datetype)
                col = new BsonDocument(column.Name, Convert.ToDateTime(column.Value));
            if (company.Properties == null)
                company.Properties = col;
            else
                company.Properties.AddRange(new BsonDocument(column.Name, col));
            await UpdateCompanyAsync(id, company);

        }


        public async Task<Company> AddCompanyAsync(Company company)
        {
            await dbcontext.Companies.InsertOneAsync(company);
            return company;
        }


        public async Task<IEnumerable<Company>> GetAllCompanyAsync()
        {
            List<Company> companies = await dbcontext.Companies.AsQueryable<Company>().ToListAsync();
            return (companies);
        }

        public async Task<IEnumerable<Company>> GetByFilterAsync(string filterjson)
        {

            JObject JFilters = JObject.Parse(filterjson);
            var companybuldire = Builders<Company>.Filter;
            FilterDefinition<Company> companyfilert = FilterDefinition<Company>.Empty;
            foreach (var JFilter in JFilters)
            {
                companyfilert = companyfilert & companybuldire.Eq(JFilter.Key, ((Newtonsoft.Json.Linq.JValue)JFilter.Value).Value);

            }
            var result = await dbcontext.Companies.Find(companyfilert).ToListAsync();
            return result;
        }

        public async Task<Company> GetCompanyAsync(string CompanyId)
        {

            var companyfilter = Builders<Company>.Filter.Eq("ID", CompanyId);
            var result = await dbcontext.Companies.Find(companyfilter).FirstOrDefaultAsync();
            return (result);
        }

        public async Task<DeleteResult> RemoveAllAsync()
        {
            var result = await dbcontext.Companies.DeleteManyAsync(new BsonDocument());
            return result;
        }

        public async Task<DeleteResult> RemoveCompanyAsync(string Id)
        {
            var filter = Builders<Company>.Filter.Eq("_id", ObjectId.Parse(Id));
            var result = await dbcontext.Companies.DeleteOneAsync(filter);
            return result;
        }

        public async Task<Company> UpdateCompanyAsync(string id, Company company)
        {
            var filter = Builders<Company>.Filter.Eq("_id", ObjectId.Parse(id));
            await dbcontext.Companies.ReplaceOneAsync(filter, company);
            return company;

        }





    }
}