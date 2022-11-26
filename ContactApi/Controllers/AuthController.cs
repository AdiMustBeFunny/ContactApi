using ContactApi.ApiModels;
using ContactApi.Controllers.Base;
using Logic.Command.User.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var command = new LoginCommand()
            {
                Username = loginModel.Username,
                Password = loginModel.Password
            };

            var result = await _mediator.Send(command);
            var response = BuildResponse(result);

            return response;
        }
    }
}
