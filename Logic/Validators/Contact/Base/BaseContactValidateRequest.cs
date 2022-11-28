using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Validators.Contact.Base
{
    public class BaseContactValidateRequest : IRequest<Request<BaseContactValidateResult>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public string CategoryId { get; set; }
        public string CategoryText { get; set; }
    }
}
