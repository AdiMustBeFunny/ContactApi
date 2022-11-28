using Database;
using Logic.Validators.Contact.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Validators.Contact.Edit
{
    public class EditContactValidateRequestHandler : IRequestHandler<EditContactValidateRequest, Request<EditContactValidateResult>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public EditContactValidateRequestHandler(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Request<EditContactValidateResult>> Handle(EditContactValidateRequest request, CancellationToken cancellationToken)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == request.Id);

            if(contact == null)
            {
                return Request<EditContactValidateResult>.Failure("This contact doesn't exist in the database");
            }

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
                return Request<EditContactValidateResult>.Failure(baseContactValidateResult.PropertyErrors);
            }

            var contactWithSameEmail = await _context.Contacts.FirstOrDefaultAsync(c => c.Email.ToLower() == request.Email.ToLower() && c.Id != request.Id);

            if (contactWithSameEmail != null)
            {
                return Request<EditContactValidateResult>.Failure("Email", "User with given email already exists!");
            }

            return Request<EditContactValidateResult>.Success(new EditContactValidateResult());
        }
    }
}
