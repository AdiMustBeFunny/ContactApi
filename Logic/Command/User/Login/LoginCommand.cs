using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Command.User.Login
{
    public class LoginCommand :IRequest<Request<LoginCommandResult>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
