﻿using Companies.Lib;
using Companies.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Companies.Services
{
    public  interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompanyAsync();
        Task<IEnumerable<Company>> GetByFilterAsync(string filterjson);
        Task<Company> GetCompanyAsync(string CompanyId);
        Task<Company> AddCompanyAsync( Company company);
        Task<Company> UpdateCompanyAsync(string id ,Company company);
        Task<DeleteResult> RemoveCompanyAsync(string Id);
        Task<DeleteResult> RemoveAllAsync();

        Task AddPropertyToCompanyDocumentAsync(string id, PropertyType column);



      

    }
}
