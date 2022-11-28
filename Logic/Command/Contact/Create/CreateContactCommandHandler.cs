using Database;
using Logic.Validators.Contact;
using Logic.Validators.Contact.Create;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Command.Contact.Create
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Request<CreateContactCommandResult>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;
        private readonly IPasswordHasher<IdentityUser> _passwordHasher;

        public CreateContactCommandHandler(ApplicationDbContext context, IMediator mediator, IPasswordHasher<IdentityUser> passwordHasher)
        {
            _context = context;
            _mediator = mediator;
            this._passwordHasher = passwordHasher;
        }

        public async Task<Request<CreateContactCommandResult>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var validateContactRequest = new CreateContactValidateRequest()
            {
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
                return Request<CreateContactCommandResult>.Failure(validateResult.PropertyErrors);
            }

            string hashedPassword = _passwordHasher.HashPassword(new IdentityUser(), request.Password);

            var contact = new Model.Contact()
            {
                BirthDate = request.BirthDate,
                CategoryId = request.CategoryId,
                CategoryText = request.CategoryText,
                Email = request.Email,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Surname = request.Surname,
                X_CreateTime = DateTime.Now,
                Password = hashedPassword
            };

            await _context.AddAsync(contact);
            await _context.SaveChangesAsync();

            var result = new CreateContactCommandResult()
            {
                Id = contact.Id
            };

            return Request<CreateContactCommandResult>.Success(result);
        }
    }
}
