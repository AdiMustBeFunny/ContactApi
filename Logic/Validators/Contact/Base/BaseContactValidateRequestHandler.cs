using Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Model.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Logic.Validators.Contact.Base
{
    public class BaseContactValidateRequestHandler : IRequestHandler<BaseContactValidateRequest, Request<BaseContactValidateResult>>
    {
        private readonly ApplicationDbContext _context;

        public BaseContactValidateRequestHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Request<BaseContactValidateResult>> Handle(BaseContactValidateRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return Request<BaseContactValidateResult>.Failure("Name", "Name cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(request.Surname))
            {
                return Request<BaseContactValidateResult>.Failure("Surname", "Surname cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                return Request<BaseContactValidateResult>.Failure("PhoneNumber", "Phone number cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return Request<BaseContactValidateResult>.Failure("Email", "Email cannot be empty");
            }

            if (!new EmailAddressAttribute().IsValid(request.Email))
            {
                return Request<BaseContactValidateResult>.Failure("Email", "This is not a valid email");
            }

            //var contactWithSameEmail = await _context.Contacts.FirstOrDefaultAsync(c => c.Email.ToLower() == request.Email);

            //if (contactWithSameEmail != null)
            //{
            //    return Request<CreateContactValidateResult>.Failure("Email", "User with given email already exists!");
            //}

            if (request.BirthDate == DateTime.MinValue || request.BirthDate < DateTime.Today.AddYears(-100) || request.BirthDate > DateTime.Today.AddYears(-4))
            {
                return Request<BaseContactValidateResult>.Failure("BirthDate", "Select a valid birthdate");
            }

            if (request.CategoryId == CategoryIdProvider.CustomCategoryId)
            {
                if (string.IsNullOrWhiteSpace(request.CategoryText))
                {
                    return Request<BaseContactValidateResult>.Failure("CategoryText", "Write your custom category here");
                }
            }
            else
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.CategoryId);

                if (category == null)
                {
                    return Request<BaseContactValidateResult>.Failure("CategoryId", "There is no such category");
                }

                var categoryhasSubCategories = await _context.Categories.AnyAsync(c => c.ParentCategoryId == request.CategoryId);

                if (categoryhasSubCategories)
                {
                    return Request<BaseContactValidateResult>.Failure("CategoryId",
                        "You cannot choose a category that has subcategories. Make sure to pick one of the subcategories");
                }
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return Request<BaseContactValidateResult>.Failure("Password", "Password cannot be empty");
            }

            if (request.Password.Length < 8)
            {
                return Request<BaseContactValidateResult>.Failure("Password", "Password must have 8 or more characters");
            }

            //validate password
            bool lowerCaseCharacter = false;
            bool upperCaseCharacter = false;
            bool numberCharacter = false;
            bool specialCharacter = false;

            foreach (char c in request.Password)
            {
                if (c >= 'a' && c <= 'z')
                {
                    lowerCaseCharacter = true;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    upperCaseCharacter = true;
                }
                else if (c >= '0' && c <= '9')
                {
                    numberCharacter = true;
                }
                else if (".,/;[]-=+@#$%&*(){}".IndexOf(c) != -1)
                {
                    specialCharacter = true;
                }
            }

            if (!(lowerCaseCharacter && upperCaseCharacter && numberCharacter && specialCharacter))
            {
                return Request<BaseContactValidateResult>.Failure("Password",
                    "Password must contain one lowercase and uppercase letter, one number and one special character .,/;[]-=+@#$%&*(){}");
            }

            return Request<BaseContactValidateResult>.Success(new BaseContactValidateResult());
        }
    }
}
