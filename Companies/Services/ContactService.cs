using Companies.App_Start;
using Companies.Lib;
using Companies.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Companies.Services
{
    public class ContactService : IContactService
    {
        private MongoDbContext dbcontext;

        public ContactService(IConfiguration config)
        {
            dbcontext = new MongoDbContext(config);
        }

        public async Task AddPropertyToContactDocumentAsync(string id, PropertyType column)
        {

            Contact contact = await GetContactAsync(id);
            var col = new BsonDocument();
            if (column.DataType == PropertyDataType.stringtype)
                col = new BsonDocument(column.Name, column.Value);
            else if (column.DataType == PropertyDataType.Intype)
                col = new BsonDocument(column.Name, Convert.ToInt32(column.Value));
            else if (column.DataType == PropertyDataType.Datetype)
                col = new BsonDocument(column.Name, Convert.ToDateTime(column.Value));
            if (contact.Properties == null)
                contact.Properties = col;
            else
                contact.Properties.AddRange(new BsonDocument(column.Name, col));
            await UpdateContactASync(id, contact);
        }

        public async Task<Contact> AddContactAsync(Contact contact)
        {
            await dbcontext.Contacts.InsertOneAsync(contact);
            return contact;
        }

        public async Task<IEnumerable<Contact>> GetAllContactAsync()
        {
            List<Contact> contacts = await dbcontext.Contacts.AsQueryable<Contact>().ToListAsync();
            return (contacts);
        }

        public async Task<Contact> GetContactAsync(string Id)
        {
            var filter = Builders<Contact>.Filter.Eq("ID", Id);
            var result = await dbcontext.Contacts.Find(filter).FirstOrDefaultAsync();
            return (result);
        }

        public async Task<DeleteResult> RemoveContactAsync(string Id)
        {
            var filter = Builders<Contact>.Filter.Eq("_id", ObjectId.Parse(Id));
            var result = await dbcontext.Contacts.DeleteOneAsync(filter);
            return result;
        }

        public async Task<Contact> UpdateContactASync(string id, Contact contact)
        {
            var filter = Builders<Contact>.Filter.Eq("_id", ObjectId.Parse(id));
            await dbcontext.Contacts.ReplaceOneAsync(filter, contact);

            return contact;
        }

    }
}
