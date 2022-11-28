using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Query.Contact.GetAll
{
    internal class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, Request<IEnumerable<ContactListItemDto>>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllContactsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Request<IEnumerable<ContactListItemDto>>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
        {
            var contacts = await _context.Contacts
                .Select(c => new ContactListItemDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Surname = c.Surname
                })
                .ToListAsync();

            return Request<IEnumerable<ContactListItemDto>>.Success(contacts);
        }
    }
}
