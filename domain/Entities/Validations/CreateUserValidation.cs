﻿using FluentValidation;

namespace domain.Entities.Validations
{
    public class CreateUserValidation : AbstractValidator<User>
    {
        public CreateUserValidation()
        {
            RuleFor(user => user.Email).NotEmpty().EmailAddress().Length(1, 100);
            RuleFor(user => user.Password).NotEmpty();
            RuleFor(user => user.FirstName).NotEmpty().Length(1, 100);
            RuleFor(user => user.LastName).NotEmpty().Length(1, 100);
        }
    }
}