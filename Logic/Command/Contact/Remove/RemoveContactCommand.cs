using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Command.Contact.Remove
{
    public class RemoveContactCommand : IRequest<Request<RemoveContactCommandResult>>
    {
        public string Id { get; set; }
    }
}
