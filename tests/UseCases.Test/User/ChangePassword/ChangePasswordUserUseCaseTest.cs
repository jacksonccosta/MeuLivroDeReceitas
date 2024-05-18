using CommonTestUtilities;
using CommonTestUtilities.Repositories;
using FluentAssertions;
using MyRecipeBook.Application;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ResourcesMessages;

namespace UseCases.Test.User.ChangePassword;

public class ChangePasswordUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        (var user, var password) = UserBuilder.Build();

        var request = RequestChangePasswordJsonBuilder.Build();
        request.CurrentPassword = password;

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => { await useCase.Execute(request); };

        await act.Should().NotThrowAsync();

        var passwordEncripter = PasswordEncripterBuilder.Build();

        user.Password.Should().Be(passwordEncripter.Encrypt(request.NewPassword));
    }

    [Fact]
    public async Task Error_NewPassword_Empyt()
    {
        (var user, var password) = UserBuilder.Build();

        var request = new RequestChangePasswordJson
        {
            CurrentPassword = password,
            NewPassword = string.Empty
        };

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => { await useCase.Execute(request); };

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorMessages.Count == 1 &&
                       e.ErrorMessages.Contains(ResourceMessagesException.PASSWORD_EMPTY));

        var passwordEncripter = PasswordEncripterBuilder.Build();

        user.Password.Should().Be(passwordEncripter.Encrypt(password));
    }

    [Fact]
    public async Task Error_CurrentPassword_Different()
    {
        (var user, var password) = UserBuilder.Build();

        var request = RequestChangePasswordJsonBuilder.Build();

        var useCase = CreateUseCase(user);

        Func<Task> act = async () => { await useCase.Execute(request); };

        (await act.Should().ThrowAsync<ErrorOnValidationException>())
            .Where(e => e.ErrorMessages.Count == 1 &&
                                  e.ErrorMessages.Contains(ResourceMessagesException.PASSWORD_INVALID));

        var passwordEncripter = PasswordEncripterBuilder.Build();

        user.Password.Should().Be(passwordEncripter.Encrypt(password));
    }

    private static ChangePasswordUseCase CreateUseCase(MyRecipeBook.Domain.User user)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userUpdateRepository = new UserUpdateOnlyRepositoryBuilder().GetById(user).Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var passwordEncripter = PasswordEncripterBuilder.Build();

        return new ChangePasswordUseCase(loggedUser, userUpdateRepository, passwordEncripter, unitOfWork);
    }
}
