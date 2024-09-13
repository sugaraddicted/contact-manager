using ContactManager.Core.Model;
using CsvHelper.Configuration;

namespace ContactManager.Maps;
public class ContactMap : ClassMap<Contact>
{
    public ContactMap()
    {
        Map(m => m.Name);
        Map(m => m.DateOfBirth);
        Map(m => m.Married);
        Map(m => m.Phone);
        Map(m => m.Salary);
    }
}