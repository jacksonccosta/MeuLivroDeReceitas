using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions.ResourcesMessages;

namespace MyRecipeBook.Application.UseCases;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        RuleFor(user => user.Email).EmailAddress().NotEmpty().WithMessage(ResourceMessagesException.EMAIL_INVALID);
        RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesException.PASSWORD_INVALID);
    }
}
