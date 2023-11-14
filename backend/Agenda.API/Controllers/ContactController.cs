using Microsoft.AspNetCore.Mvc;
using Agenda.Infra.Data;
using Agenda.Infra.Repositories;
using Agenda.Domain.Entities;

namespace Agenda.API.Controllers
{
    [ApiController]
    [Route(template: "api/v1/contacts")]
    public class ContactController : ControllerBase
    {
        private readonly AgendaDbContext _context;
        private readonly IContactRepository _contactRepository;

        public ContactController(AgendaDbContext context, IContactRepository contactRepository)
        {
            _context = context;
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactRepository.GetAllAsync();

            return Ok(contacts);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetById(int id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            var newContact = await _contactRepository.CreateAsync(contact);

            return CreatedAtAction(nameof(GetById), new { id = contact.Id }, newContact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }
            var contactToUpdate = await _contactRepository.GetByIdAsync(id);

            if (contactToUpdate != null)
            {
                await _contactRepository.UpdateAsync(id, contact);
                return Ok(contactToUpdate);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contactToDelete = await _contactRepository.GetByIdAsync(id);

            if (contactToDelete != null)
            {
                await _contactRepository.DeleteAsync(id);
                return Ok(contactToDelete);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
