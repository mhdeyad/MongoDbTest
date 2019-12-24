using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Companies.Lib;
using Companies.Models;
using Companies.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Companies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private ICompanyService _CompanyService;

        public ContactController(ICompanyService companyService)
        {


            _CompanyService = companyService;
        }



        // GET: api/Contact
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var contacts = await _CompanyService.GetAllContact();
         
            return Json(contacts);
        }
        // GET: api/Contact/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {

            var contact = await _CompanyService.GetContact(id);

            return Json(contact);
        }

        // POST: api/Contact
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contact contact)
        {
            try
            {

                await _CompanyService.AddContact(contact);

                return Ok(Json(contact));
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT: api/Contact/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Contact contact)
        {
            try
            {

                var result = await _CompanyService.UpdateContact(id, contact);

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


                await _CompanyService.AddColumnToContactCollection(id, column);


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
                _CompanyService.RemoveContact(id);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

