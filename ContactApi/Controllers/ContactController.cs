using ContactApi.ApiModels;
using ContactApi.Controllers.Base;
using Logic.Command.Contact.Create;
using Logic.Command.Contact.Edit;
using Logic.Command.Contact.Remove;
using Logic.Query.Contact.GetAll;
using Logic.Query.Contact.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateContactModel model)
        {
            var command = new CreateContactCommand()
            {
                BirthDate = model.BirthDate,
                CategoryId = model.CategoryId,
                CategoryText = model.CategoryText,
                Email = model.Email,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Surname = model.Surname,
                Password = model.Password
            };

            var result = await _mediator.Send(command);

            return BuildResponse(result);
        }
        [Authorize]
        [HttpPost("edit")]
        public async Task<IActionResult> Edit(EditContactModel model)
        {
            var command = new EditContactCommand()
            {
                Id = model.Id,
                BirthDate = model.BirthDate,
                CategoryId = model.CategoryId,
                CategoryText = model.CategoryText,
                Email = model.Email,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Surname = model.Surname,
                Password = model.Password
            };

            var result = await _mediator.Send(command);

            return BuildResponse(result);
        }
        [Authorize]
        [HttpPost("remove")]
        public async Task<IActionResult> Remove(string id)
        {
            var command = new RemoveContactCommand()
            {
                Id = id
            };

            var result = await _mediator.Send(command);

            return BuildResponse(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllContactsQuery();

            var result = await _mediator.Send(query);

            return BuildResponse(result);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetById(string id)
        {
            var query = new GetContactByIdQuery()
            {
                Id=id
            };

            var result = await _mediator.Send(query);

            return BuildResponse(result);
        }


    }
}
