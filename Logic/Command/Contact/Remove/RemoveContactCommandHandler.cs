using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Command.Contact.Remove
{
    public class RemoveContactCommandHandler : IRequestHandler<RemoveContactCommand, Request<RemoveContactCommandResult>>
    {
        private readonly ApplicationDbContext _context;

        public RemoveContactCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Request<RemoveContactCommandResult>> Handle(RemoveContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == request.Id);

            if(contact == null)
            {
                return Request<RemoveContactCommandResult>.Failure("This contact doesn't exist in the database");
            }

            contact.X_Remove_Time = DateTime.Now;

            await _context.SaveChangesAsync();

            return Request<RemoveContactCommandResult>.Success(new RemoveContactCommandResult());
        }
    }
}
