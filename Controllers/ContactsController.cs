using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCoreTwo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyCoreTwo.Controllers
{
    /// <summary>
    /// Manage Contact for HAL Inc co.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contacts;
        ILogger _logger;

        public ContactsController(IContactRepository contacts, ILogger<ContactsController> logger)
        {
            _contacts = contacts;
            _logger = logger;
        }

        // GET api/contacts
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return _contacts.GetAll();
        }


        [HttpGet("full")]
        public IEnumerable<Contact> GetAll()
        {
            _logger.LogInformation("HAL Executing Contacts/Get full info");
            _logger.LogWarning("THIS is may warning to you");
            _logger.LogError("YOU are busted!......");
            return _contacts.GetAll();
        }


        #region missing404docs
        // GET api/contacts/{guid}
        [HttpGet("{id}", Name = "GetByItemId")]
        [ProducesResponseType(typeof(Contact), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Contact), StatusCodes.Status404NotFound)]
        public IActionResult Get(string id)
        {
            var contact = _contacts.Get(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }
        #endregion

        // POST api/contacts
        [HttpPost]
        public IActionResult Post(Contact contact)
        {
            _contacts.Add(contact);

            return CreatedAtRoute("GetByItemId", new { id = contact.ID }, contact);
        }

        // PUT api/contacts/{guid}
        [HttpPut("{id}")]
        public IActionResult Put(string id, Contact contact)
        {
            if (ModelState.IsValid && id == contact.ID)
            {
                var contactToUpdate = _contacts.Get(id);

                if (contactToUpdate != null)
                {
                    _contacts.Update(contact);
                    return NoContent();
                }

                return NotFound();
            }

            return BadRequest();
        }

        // DELETE api/contacts/{guid}
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Contact), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Contact), StatusCodes.Status404NotFound)]
        public IActionResult Delete(string id)
        {
            var contact = _contacts.Get(id);

            if (contact == null)
            {
                return NotFound();
            }

            _contacts.Remove(id);

            return NoContent();
        }
    }
}
