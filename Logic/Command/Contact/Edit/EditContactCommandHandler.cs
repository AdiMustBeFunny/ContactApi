using Database;
using Logic.Command.Contact.Create;
using Logic.Validators.Contact;
using Logic.Validators.Contact.Edit;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Command.Contact.Edit
{
    public class EditContactCommandHandler : IRequestHandler<EditContactCommand, Request<EditContactCommandResult>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public EditContactCommandHandler(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Request<EditContactCommandResult>> Handle(EditContactCommand request, CancellationToken cancellationToken)
        {
            var validateContactRequest = new EditContactValidateRequest()
            {
                Id = request.Id,
                BirthDate = request.BirthDate,
                CategoryId = request.CategoryId,
                CategoryText = request.CategoryText,
                Email = request.Email,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Surname = request.Surname,
                Password = request.Password
            };

            var validateResult = await _mediator.Send(validateContactRequest);

            if (!validateResult.IsSuccessful)
            {
                return Request<EditContactCommandResult>.Failure(validateResult.ErrorMessage,validateResult.PropertyErrors);
            }

            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: request.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            var contact = await _context.Contacts.FirstAsync(c => c.Id == request.Id);

            contact.Name = request.Name;
            contact.Email = request.Email;
            contact.Surname = request.Name;
            contact.PhoneNumber = request.PhoneNumber;
            contact.Password = hashedPassword;
            contact.CategoryId = request.CategoryId;
            contact.CategoryText = request.CategoryText;
            contact.BirthDate = request.BirthDate;

            _context.SaveChanges();

            return Request<EditContactCommandResult>.Success(new EditContactCommandResult());
        }
    }
}
