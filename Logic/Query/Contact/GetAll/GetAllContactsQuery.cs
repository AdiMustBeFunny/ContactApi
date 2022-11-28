using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Query.Contact.GetAll
{
    public class GetAllContactsQuery : IRequest<Request<IEnumerable<ContactListItemDto>>>
    {
    }
}
