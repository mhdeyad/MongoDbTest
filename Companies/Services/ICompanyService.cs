using Companies.Lib;
using Companies.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Companies.Services
{
   public  interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompany();
        Task<IEnumerable<Company>> GetByFilter(string filterjson);
        Task<Company> GetCompany(string CompanyId);
        Task<Company> AddCompany( Company company);
        Task<Company> UpdateCompany(string id ,Company company);
        Task<DeleteResult> RemoveCompany(string Id);
        Task<DeleteResult> RemoveAll();

        Task AddColumnToCompanyCollection(string id, FieldType column);



        Task AddColumnToContactCollection(string id, FieldType column);

        Task<IEnumerable<Contact>> GetAllContact();
        Task<Contact> GetContact(string Id);
        Task<Contact> AddContact(Contact contact);
        Task<Contact> UpdateContact(string id, Contact contact);
        Task<DeleteResult> RemoveContact(string Id);

    }
}
