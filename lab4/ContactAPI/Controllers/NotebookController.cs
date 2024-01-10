using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Laba4.Data;
using Laba4.Models;

namespace Laba4.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ApiContext _dbContext;

        public ContactController(ApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllContacts()
        {
            var contacts = _dbContext.Contacts.ToList();
            return new JsonResult(contacts);
        }

        [HttpPost]
        public IActionResult AddContact(Contacts contact)
        {
            var existingContact = _dbContext.Contacts.FirstOrDefault(c => c.ID == contact.ID);

            if (existingContact == null)
            {
                _dbContext.Contacts.Add(contact);
            }
            else
            {
                _dbContext.Entry(existingContact).CurrentValues.SetValues(contact);
            }

            _dbContext.SaveChanges();
            return new JsonResult(contact);
        }

        [HttpGet]
        public IActionResult GetContact(string id)
        {
            var contact = _dbContext.Contacts.Find(id);

            if (contact == null)
            {
                return NotFound();
            }

            return new JsonResult(contact);
        }
    }
}