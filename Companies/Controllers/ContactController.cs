using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private IContactService _ContactService;

        public ContactController(IContactService contactService)
        {
            _ContactService = contactService;
        }

        /// <summary>
        /// Get all Contact data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var contacts = await _ContactService.GetAllContactAsync();
                return Json(contacts);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Get contact data by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var contact = await _ContactService.GetContactAsync(id);
                return Json(contact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// add one contact data to contact collection
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contact contact)
        {
            try
            {
                await _ContactService.AddContactAsync(contact);
                return Ok(Json(contact));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// update one contact data by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Contact contact)
        {
            try
            {
                var result = await _ContactService.UpdateContactASync(id, contact);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Add one column  with value to contact document by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        [HttpPost("AddColumn{id}")]
        public async Task<IActionResult> AddColumn(string id, [FromForm] FieldType column)
        {
            try
            {
                await _ContactService.AddColumnToContactCollectionAsync(id, column);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// delete on document from contact collection by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _ContactService.RemoveContactAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}

