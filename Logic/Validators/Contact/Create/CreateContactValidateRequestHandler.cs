using Database;
using Logic.Command.Contact.Create;
using Logic.Validators.Contact.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Validators.Contact.Create
{
    public class CreateContactValidateRequestHandler : IRequestHandler<CreateContactValidateRequest, Request<CreateContactValidateResult>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public CreateContactValidateRequestHandler(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Request<CreateContactValidateResult>> Handle(CreateContactValidateRequest request, CancellationToken cancellationToken)
        {
            var baseContactValidateRequest = new BaseContactValidateRequest()
            {
                Name = request.Name,
                BirthDate = request.BirthDate,
                CategoryId = request.CategoryId,
                CategoryText = request.CategoryText,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                Surname = request.Surname
            };

            var baseContactValidateResult = await _mediator.Send(baseContactValidateRequest);

            if (!baseContactValidateResult.IsSuccessful)
            {
                return Request<CreateContactValidateResult>.Failure(baseContactValidateResult.PropertyErrors);
            }

            var contactWithSameEmail = await _context.Contacts.FirstOrDefaultAsync(c => c.Email.ToLower() == request.Email.ToLower());

            if (contactWithSameEmail != null)
            {
                return Request<CreateContactValidateResult>.Failure("Email", "User with given email already exists!");
            }

            return Request<CreateContactValidateResult>.Success(new CreateContactValidateResult());
        }
    }
}
