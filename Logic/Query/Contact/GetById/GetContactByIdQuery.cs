using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Query.Contact.GetById
{
    public class GetContactByIdQuery : IRequest<Request<ContactDto>>
    {
        public string Id { get; set; }
    }
}
