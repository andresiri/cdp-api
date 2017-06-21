﻿using api.Context.Transaction;
using domain.Entities.Model;
using domain.Lib;
using FluentValidation;
using domain.Entities.Validations.Login;
using domain.Entities.Exceptions;
using domain.Entities.Enum;

namespace api.Op.Login
{
    public class LoginOp : Operation<LoginModel>
    {
        readonly IUnitOfWork _unitOfWork;

        public LoginOp(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override AbstractValidator<LoginModel> GetValidation(LoginModel entity)
        {
            return new LoginValidation();
        }

        public override object Process(LoginModel entity)
        {
            var user = _unitOfWork.UserRepository.GetByEmailAddress(entity.Email);

            if (user != null)
            {
                var hashPassword = Password.Hash(entity.Password, entity.Email);

                if (hashPassword.Equals(user.Password))
                {
                    return user;
                }

                throw new CustomException(ExceptionMessage.INVALID_USERNAME_PASSWORD, ExceptionType.LOGIN_ERROR);
            }

            throw new CustomException(ExceptionMessage.INVALID_USERNAME_PASSWORD, ExceptionType.LOGIN_ERROR);
        }
    }
}
