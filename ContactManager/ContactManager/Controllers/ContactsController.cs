using ContactManager.Core.Interfaces;
using ContactManager.Core.Model;
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text;
using ContactManager.Maps;

namespace ContactManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController(IContactRepository contactRepository) : Controller
{

    [HttpGet]
    public async Task<IActionResult> GetContacts()
    {
        var contacts = await contactRepository.GetAllAsync();
        return Ok(contacts);
    }

    [HttpPost]
    public async Task<IActionResult> UploadContacts(IFormFile csv)
    {
        if (csv == null || csv.Length == 0)
        {
            return BadRequest("CSV file is required.");
        }

        var contacts = new List<Contact>();

        using (var stream = new StreamReader(csv.OpenReadStream(), Encoding.UTF8))
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null,
            };

            using (var csvReader = new CsvReader(stream, csvConfig))
            {
                csvReader.Context.RegisterClassMap<ContactMap>();
                contacts = csvReader.GetRecords<Contact>().ToList();
            }
        }

        await contactRepository.Upload(contacts);

        return Ok("Contacts uploaded successfully");
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateContact(Guid id, Contact contact)
    {
        await contactRepository.Update(contact);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(Guid id)
    {
        await contactRepository.Delete(id);
        return Ok();
    }
}