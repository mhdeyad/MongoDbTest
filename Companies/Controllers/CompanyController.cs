using Companies.Lib;
using Companies.Models;
using Companies.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
       /// <summary>
       /// Get all Company data
       /// </summary>
       /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var companies = await _CompanyService.GetAllCompanyAsync();
                return Json(companies);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
 
      /// <summary>
      /// Get one Company data by Id
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var company = await _CompanyService.GetCompanyAsync(id);
                return Json(company);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Add one company
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
      
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Company company)
        {
            try
            {
                await _CompanyService.AddCompanyAsync(company);
                return Ok(Json(company));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Update One company
        /// </summary>
        /// <param name="id"></param>
        /// <param name="company"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Company company)
        {
            try
            {
                var result = await _CompanyService.UpdateCompanyAsync(id, company);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Add one column to company document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        [HttpPost("AddColumn{id}")]
        public async Task<IActionResult> AddColumn(string id, [FromForm] FieldType column)
        {
            try
            {
                await _CompanyService.AddColumnToCompanyCollectionAsync(id, column);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// delete company by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _CompanyService.RemoveCompanyAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        /// <summary>
        /// filter company data by pass json text
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet("GetByFilter")]
        public async Task<IActionResult> GetByFilter(string filter)
        {
            try
            {
                var companies = await _CompanyService.GetByFilterAsync(filter);
                return Json(companies);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
