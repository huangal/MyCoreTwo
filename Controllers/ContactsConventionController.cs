using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCoreTwo.Models;

namespace MyCoreTwo.Controllers
{
    #region snippet_ApiConventionTypeAttribute
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    public class ContactsConventionController : ControllerBase
    {
        #endregion
        private readonly IContactRepository _contacts;
        private readonly ILogger _logger;

        public ContactsConventionController(IContactRepository contacts, ILogger<ContactsConventionController> logger)
        {
            _contacts = contacts;
            _logger = logger;
        }

        // GET api/contactsconvention
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return _contacts.GetAll();
        }

        // GET api/contactsconvention/{guid}
        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<Contact> Get(string id)
        {
            _logger.LogInformation( "Getting item {ID}", id);
            var contact = _contacts.Get(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // POST api/contactsconvention
        [HttpPost]
        public IActionResult Post(Contact contact)
        {
            _contacts.Add(contact);

            return CreatedAtRoute("GetById", new { id = contact.ID }, contact);
        }

        #region snippet_ApiConventionMethod
        // PUT api/contactsconvention/{guid}
        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                             nameof(DefaultApiConventions.Put))]
        public IActionResult Update(string id, Contact contact)
        {
            var contactToUpdate = _contacts.Get(id);

            if (contactToUpdate == null)
            {
                return NotFound();
            }

            _contacts.Update(contact);

            return NoContent();
        }
        #endregion

        // DELETE api/contactsconvention/{guid}
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
