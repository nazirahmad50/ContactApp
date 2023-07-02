using Application;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ContactsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return HandleResult(await Mediator.Send(new ListContacts.Query()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            return HandleResult(await Mediator.Send(new ContactById.Query(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(ContactUpdateCreateDto productCreateDto)
        {
            return HandleResult(await Mediator.Send(new CreateContact.Command(productCreateDto)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            return HandleResult(await Mediator.Send(new DeleteContact.Command(id)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, ContactUpdateCreateDto productUpdateDto)
        {
            return HandleResult(await Mediator.Send(new UpdateContact.Command(id, productUpdateDto)));
        }
    }
}
