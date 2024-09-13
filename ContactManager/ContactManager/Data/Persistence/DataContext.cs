using ContactManager.Core.Model;
using ContactManager.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data.Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
    }

    public DbSet<Contact> Contacts { get; set; }
}