using ContactManager.Core.Interfaces;
using ContactManager.Core.Model;
using Microsoft.AspNetCore.Mvc;

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
        return Ok();
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