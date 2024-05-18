using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Services;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ResourcesMessages;

namespace MyRecipeBook.Application;

public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;

    public ChangePasswordUseCase(ILoggedUser loggedUser, IUserUpdateOnlyRepository repository, IPasswordEncripter passwordEncripter, IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
    }

    public async Task Execute(RequestChangePasswordJson request)
    {
        var loggedUser = await _loggedUser.User();
        Validate(request, loggedUser);

        var user = await _repository.GetById(loggedUser.Id);
        user.Password = _passwordEncripter.Encrypt(request.NewPassword);

        _repository.Update(user);
        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePasswordJson request, User loggedUser)
    {
        var result = new ChangePasswordValidator().Validate(request);
        var currentPassword = _passwordEncripter.Encrypt(request.CurrentPassword);

        if(currentPassword.Equals(loggedUser.Password).IsFalse())
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.PASSWORD_INVALID));

        if(result.IsValid.IsFalse())
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
    }
}
