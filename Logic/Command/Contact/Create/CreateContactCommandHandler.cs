using Database;
using Logic.Validators.Contact;
using Logic.Validators.Contact.Create;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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

        public CreateContactCommandHandler(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
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

            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); 
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: request.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

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
