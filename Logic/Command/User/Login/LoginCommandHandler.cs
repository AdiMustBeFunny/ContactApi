using Database;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Command.User.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Request<LoginCommandResult>>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<Request<LoginCommandResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if(user == null)
            {
                return Request<LoginCommandResult>.Failure("User with give username doesn't exist");
            }

            var passwordCheckPassed = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!passwordCheckPassed)
            {
                return Request<LoginCommandResult>.Failure("Incorrect Password");
            }

            var userRoles = new List<Claim>()
            {
                new Claim("Name",user.UserName),
                new Claim("Email",user.Email),
                new Claim("Id",user.Id),
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: userRoles,
                signingCredentials: new SigningCredentials(authSigningKey,SecurityAlgorithms.HmacSha256));

            return Request<LoginCommandResult>.Success(new LoginCommandResult()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            });
        }
    }
}
