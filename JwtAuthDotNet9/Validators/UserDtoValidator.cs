using FluentValidation;
using JwtAuthDotNet9.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace JwtAuthDotNet9.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("İsim boş olamaz");

            RuleFor(x => x.PasswordHash)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Şifre en az 6 karakter olmalı");

          
        }
    }
}
