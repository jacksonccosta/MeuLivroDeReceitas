using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exceptions.ResourcesMessages;

namespace MyRecipeBook.Application;

public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
{
    public UpdateUserValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        RuleFor(request => request.Email)
            .NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPYT);

        When(request => string.IsNullOrWhiteSpace(request.Email).IsFalse(), () =>
        {
            RuleFor(request => request.Email)
                .EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
        });
    }
}
