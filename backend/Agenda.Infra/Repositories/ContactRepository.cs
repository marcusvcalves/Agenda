using Agenda.Domain.Entities;
using Agenda.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Infra.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly AgendaDbContext _context;

        public ContactRepository(AgendaDbContext context)
        {
            _context = context;
        }
        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }
        public async Task<Contact> CreateAsync(Contact newContact)
        {
            _context.Contacts.Add(newContact);
            await _context.SaveChangesAsync();

            return newContact;
        }
        public async Task UpdateAsync(int id, Contact contact)
        {
            var existingContact = await GetByIdAsync(id);

            if (existingContact != null)
            {
                existingContact.FullName = contact.FullName;
                existingContact.Email = contact.Email;
                existingContact.PhoneNumber = contact.PhoneNumber;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var contactToDelete = await GetByIdAsync(id);

            if (contactToDelete != null)
            {
                _context.Contacts.Remove(contactToDelete);

                await _context.SaveChangesAsync();
            }
        }
    }
}
