using Agenda.Domain.Entities;

namespace Agenda.Infra.Repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact> GetByIdAsync(int id);
        Task<Contact> CreateAsync(Contact newContact);
        Task UpdateAsync(int id, Contact contact);
        Task DeleteAsync(int id);
    }
}
