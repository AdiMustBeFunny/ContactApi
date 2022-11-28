using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Query.Contact.GetById
{
    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, Request<ContactDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetContactByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Request<ContactDto>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            var contact = await _context.Contacts
                .Select(c => new ContactDto()
                {
                    Id = c.Id,
                    BirthDate = c.BirthDate,
                    CategoryId = c.CategoryId,
                    CategoryText = c.CategoryText,
                    Email = c.Email,
                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber,
                    Surname = c.Surname
                })
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if(contact == null)
            {
                return Request<ContactDto>.Failure("This contact doesn't exist in the database");
            }

            return Request<ContactDto>.Success(contact);
        }
    }
}
