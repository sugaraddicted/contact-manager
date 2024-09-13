using ContactManager.Core.Model;

namespace ContactManager.Core.Interfaces;

public interface IContactRepository
{
    Task<ICollection<Contact>> GetAllAsync();
    Task Upload(ICollection<Contact> contacts);
    Task Delete(Guid id);
    Task Update(Contact contact);
}