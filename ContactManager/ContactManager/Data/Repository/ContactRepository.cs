using ContactManager.Core.Interfaces;
using ContactManager.Core.Model;
using ContactManager.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data.Repository;

public class ContactRepository(DataContext context) : IContactRepository
{

    public async Task<ICollection<Contact>> GetAllAsync()
    {
        return await context.Contacts.ToListAsync();
    }

    public async Task Upload(ICollection<Contact> contacts)
    {
        await context.AddRangeAsync(contacts);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var entity = await context.Contacts.FirstOrDefaultAsync(x => x.Id == id);

        if (entity != null)
        {
            context.Contacts.Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    public async Task Update(Contact contact)
    {
        context.Update(contact);
        await context.SaveChangesAsync();
    }
}