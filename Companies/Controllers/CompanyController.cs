using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Companies.App_Start;
using Companies.Lib;
using Companies.Models;
using Companies.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Companeis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
      
    
        private ICompanyService _CompanyService;

        public CompanyController(ICompanyService companyService)
        {
          
          
            _CompanyService = companyService;
        }
        // GET: api/Company
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var companies = await _CompanyService.GetAllCompany();
           // List<Company> companies = companycollection.AsQueryable<Company>().ToList();
            return Json(companies);
        }
        [HttpGet("GetByFilter")]
        public async Task<IActionResult> GetByFilter(string filter)
        {
            var companies = await _CompanyService.GetByFilter(filter);
            
            return Json(companies);
        }
        // GET: api/Company/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
          
            var company = await _CompanyService.GetCompany(id);
        
            return Json(company);
        }

        // POST: api/Company
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Company company)
        {
            try
            {
              
              await   _CompanyService.AddCompany(company);
               
              return Ok(Json(company));
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT: api/Company/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Company company)
        {
            try
            {

                var result = await _CompanyService.UpdateCompany(id, company);
              
                return Ok(result);

            }
            catch (Exception ex)
            {

                return Ok(ex.Message);
            }
        }
        [HttpPost("AddColumn{id}")]
        public async Task<IActionResult> AddColumn(string id, [FromForm] FieldType column)
        {
            try
            {


              await  _CompanyService.AddColumnToCompanyCollection(id, column);
               

              return Ok();

            }
            catch (Exception ex)
            {

                return Ok(ex.Message);
            }
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _CompanyService.RemoveCompany(id);
              
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
