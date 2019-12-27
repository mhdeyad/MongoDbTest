using Companies.Lib;
using Companies.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Companies.Services
{
    public  interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContactAsync();
        Task<Contact> GetContactAsync(string Id);
        Task<Contact> AddContactAsync(Contact contact);
        Task<Contact> UpdateContactASync(string id, Contact contact);
        Task<DeleteResult> RemoveContactAsync(string Id);
        Task AddPropertyToContactDocumentAsync(string id, PropertyType column);
    }
}
